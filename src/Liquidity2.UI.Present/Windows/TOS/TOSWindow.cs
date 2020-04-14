using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.EventBus.EventObserver;
using Liquidity2.UI.Components.UsersControl;
using Liquidity2.UI.Core;
using Liquidity2.UI.Services.DTO;
using Liquidity2.UI.Services.TOS;
using Liquidity2.UI.Services.TOS.Events;
using Liquidity2.UI.Windows.TOS.EventHandlers;
using Liquidity2.UI.Windows.TOS.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Liquidity2.UI.Windows.TOS
{
    public class TOSWindow : Window,
        IEventHandler<TOSDataIncomingEvent>,
        IEventHandler<L2DataIncomingEvent>,
        //IEventHandler<TosGroupSubscribeEvent>,
        //IEventHandler<SelfSelectSearchEvent>,
        IEventHandler<L2DataQueryEvent>,
        IEventHandler<TOSDataQueryEvent>,
        IPrecisionChangeEventHandler,
         ITemplateLoader, IEventObserver
    {
        private readonly ITOSService _tosService;
        private readonly IWindowCommonBehavior _windowCommonBehavior;
        private readonly IEventBus _bus;
        private readonly ITOSWindowDataMapper _mapper;
        private ITOSMarketObsever _tosSubjectObserver;
        private IL2MarketObsever _l2SubjectObserver;
        private bool _groupActivate;
        private string[] _viewHeader;

        public TOSWindow(IWindowCommonBehavior windowCommonBehavior, ITOSService tosService, IEventBus bus, ITOSWindowDataMapper mapper)
        {
            _tosService = tosService;
            windowCommonBehavior.SetEffectWindow(this);
            windowCommonBehavior.WindowSearchChanged += WindowSearchChanged;
            windowCommonBehavior.WindowGroupChanged += WindowGroupChanged;
            _windowCommonBehavior = windowCommonBehavior;
            WindowId = Guid.NewGuid();
            _tosService = tosService;
            _bus = bus;
            _mapper = mapper;

            //ViewModel初始化
            TosVM = new TOSViewModel(windowCommonBehavior)
            {
                Group = "/",
                BuyPercentage = "50%",
                SellPercentage = "50%",
                BuyHistogram = new GridLength(50, GridUnitType.Star),
                SellHistogram = new GridLength(50, GridUnitType.Star),
                AmountHeader = "数量",
                PriceHeader = "价格",
                TosTitle = "行情",
                NowPrecision = 0
            };

            //初始化命令
            AddCommandBinding();

            DataContext = this;
        }

        public TOSViewModel TosVM { get; }
        protected Guid WindowId { get; }

        private void AddCommandBinding()
        {
            this.AddCommandBinding(TosVM.CreateBuyOrderCmd, CanExecute, CreateOrder_Executed);
            this.AddCommandBinding(TosVM.CreateSellOrderCmd, CanExecute, CreateSellOrder_Executed);
            this.AddCommandBinding(TosVM.PrecisionChangeCmd, CanExecute, PrecisionChange_Executed);
            this.AddCommandBinding(TosVM.StarButtonClickCmd, CanExecute, StarButtonClick_Executed);
        }

        //点击星星按钮
        private void StarButtonClick_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.OriginalSource is StarButton button)
            {
                if (button.StarIsSelfSelect == true)
                {
                    //selfSelectService.RemoveMarkSymbol(TosVM.Symbol);
                }
                else
                {
                    //selfSelectService.AddMarkSymbol(TosVM.Symbol);
                }
                button.StarIsSelfSelect = !button.StarIsSelfSelect;
            }
        }

        //切换精度
        private async void PrecisionChange_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is string precisionString)
            {
                var precision = TosVM.MaxPrecision - _mapper.MapToPrecisionInt(precisionString);
                TosVM.NowPrecision = precision;

                var @event = new PrecisionChangeUnsubscribeEvent(TosVM.Symbol, precision);
                await _bus.Publish(@event, CancellationToken.None);
                var precisionSubscribeEvent = new PrecisionChangeSubscribeEvent(TosVM.Symbol, precision);
                await _bus.Publish(precisionSubscribeEvent, CancellationToken.None);
            }
        }

        //创建卖单
        private void CreateSellOrder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is L2Data data)
            {
                var createOrderEvent = _mapper.MapToPlaceOrderEvent(_viewHeader[0], _viewHeader[1], data, TradeDirection.Sell);
                _bus.Publish(createOrderEvent, CancellationToken.None);
            }
        }

        //创建买单
        private void CreateOrder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is L2Data data)
            {
                var createOrderEvent = _mapper.MapToPlaceOrderEvent(_viewHeader[0], _viewHeader[1], data, TradeDirection.Buy);
                _bus.Publish(createOrderEvent, CancellationToken.None);
            }
        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        //联动按钮改变
        private void WindowGroupChanged(object sender, PropertyChangedEventArgs e)
        {
            //改变groupButton
            _groupActivate = e.PropertyName != "/";
            TosVM.Group = e.PropertyName;
        }

        //币对搜索
        private void WindowSearchChanged(object sender, PropertyChangedEventArgs e)
        {
            TosVM.Symbol = e.PropertyName.ToLower();
            TosVM.NowPrecision = 0;
            //注意：异步方法不要用Task.Wait()或者Task.Result
            //应使用ContinueWith创建匿名委托把后续方法放在匿名委托里执行
            SubscribeData().ContinueWith(task =>
            {
                // 通知其他同组窗口更变订阅
                if (_groupActivate && !string.IsNullOrEmpty(TosVM.Symbol))
                {
                    _bus.Publish(new GroupSubscribeEvent(WindowId, _tosSubjectObserver.Symbol, TosVM.Group), CancellationToken.None);
                }
            });
        }

        //订阅币对行情
        private async Task SubscribeData()
        {
            _tosSubjectObserver = await _tosService.SubscribeTosData(TosVM.Symbol, this);
            _l2SubjectObserver = await _tosService.SubscribeL2Data(TosVM.Symbol, this, TosVM.NowPrecision);
            //await selfSelectService.GetMarkSymbol();
            await _tosService.GetTOSData(TosVM.Symbol);
            await _tosService.GetL2Data(TosVM.Symbol);
        }

        /// <summary>
        /// 接受TOS订阅数据
        /// </summary>
        /// <param name="event">TOS推送事件</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Handle(TOSDataIncomingEvent @event, CancellationToken token)
        {
            //收到的信息不为当前订阅则不处理   
            if (@event.Symbol != _tosSubjectObserver.Symbol)
                return;

            //插入TOS列表中
            await Dispatcher.InvokeAsync(() =>
            {
                foreach (var item in @event.TOSItems)
                {
                    var TOSData = _mapper.MapToTOS(item);
                    RemoveListItem(TosVM.ListData);
                    TosVM.ListData.Insert(0, TOSData);
                }
            });
        }

        /// <summary>
        /// 删除TOS多余列表数据
        /// </summary>
        /// <param name="listData"></param>
        private void RemoveListItem(ObservableCollection<TOSData> listData)
        {
            if (listData.Count == 200)
            {
                listData.RemoveAt(listData.Count - 1);
            }
        }

        /// <summary>
        /// 接收L2订阅数据
        /// </summary>
        /// <param name="event">L2推送事件</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Handle(L2DataIncomingEvent @event, CancellationToken token)
        {
            //收到消息不为当前订阅则不处理
            if (@event.Symbol != _l2SubjectObserver.Symbol)
                return;

            _viewHeader = @event.Symbol.Split("-");
            //注意：操作UI控件的数据源需要UI所在线程处理
            //插入L2列表中
            await Dispatcher.InvokeAsync(() =>
            {
                var priceItem = @event.TradeItems.First();

                this.Title = $"行情:{@event.Symbol.ToUpper()}";
                if (@event.Side == TradeDirection.Buy)
                {
                    TosVM.BuyListData.Clear();
                }
                else if (@event.Side == TradeDirection.Sell)
                {
                    TosVM.SellListData.Clear();
                }

                foreach (var item in @event.TradeItems)
                {
                    var L2Data = _mapper.MapToL2(item, @event.Precision);
                    ListInsert(L2Data, @event.Side);
                }

                BindingPropertyChange();
            });
        }

        /// <summary>
        /// 绑定属性改变
        /// </summary>
        private void BindingPropertyChange()
        {
            //买卖第一档数量
            TosVM.BuyCount = FirstRankCount(TosVM.BuyListData);
            TosVM.SellCount = FirstRankCount(TosVM.SellListData);

            //买卖百分比
            TosVM.BuyPercentage = (TosVM.BuyCount / (TosVM.BuyCount + TosVM.SellCount)).ToString("0.00%");
            TosVM.SellPercentage = (TosVM.SellCount / (TosVM.BuyCount + TosVM.SellCount)).ToString("0.00%");

            //买卖柱形图
            TosVM.BuyHistogram = new GridLength(Convert.ToDouble(TosVM.BuyCount / (TosVM.BuyCount + TosVM.SellCount)), GridUnitType.Star);
            TosVM.SellHistogram = new GridLength(Convert.ToDouble(TosVM.SellCount / (TosVM.BuyCount + TosVM.SellCount)), GridUnitType.Star);

            //表头标识
            TosVM.AmountHeader = $"数量({_viewHeader[0].ToUpper()})";
            TosVM.PriceHeader = $"价格({_viewHeader[1].ToUpper()})";
        }

        //查询L2第一档的数量
        private decimal FirstRankCount(ObservableCollection<L2Data> l2Datas)
        {
            decimal firstRankAmount = 0;
            foreach (var item in l2Datas)
            {
                if (item.Group > 0) break;
                firstRankAmount += item.Amount;
            }
            return firstRankAmount;
        }

        /// <summary>
        /// 区分L2买盘或卖盘数据
        /// </summary>
        /// <param name="l2Data">L2数据</param>
        /// <param name="side">L2的买卖方向</param>
        private void ListInsert(L2Data l2Data, TradeDirection side)
        {
            //买盘数据
            if (side == TradeDirection.Buy)
            {
                ListDataInsert(l2Data, TosVM.BuyListData, side);
                TosVM.BuyTopPrice = TosVM.BuyListData[0].Price;
            }
            //卖盘数据
            else if (side == TradeDirection.Sell)
            {
                ListDataInsert(l2Data, TosVM.SellListData, side);
                TosVM.SellTopPrice = TosVM.SellListData[0].Price;
            }
        }

        /// <summary>
        /// 插入L2数据
        /// </summary>
        /// <param name="l2Data"></param>
        /// <param name="listData"></param>
        /// <param name="side"></param>
        private void ListDataInsert(L2Data l2Data, ObservableCollection<L2Data> listData, TradeDirection side)
        {
            //当初始插入时
            if (listData.Count == 0)
            {
                l2Data.Group = 0;
                listData.Insert(0, l2Data);
                return;
            }

            //寻找插入位置
            var insertPosition = FindIndex(l2Data, listData, side);
            //判断是否为重复数据
            if (IsRepeatItem(insertPosition, listData, l2Data))
            {
                listData[insertPosition - 1].Amount += l2Data.Amount;
                return;
            }
            //插入数据
            listData.Insert(insertPosition, l2Data);
            //L2的档次改动
            OnListGroupChange(insertPosition, listData);
        }

        private void OnListGroupChange(int insertPosition, ObservableCollection<L2Data> listData)
        {
            if (insertPosition > 0)
            {
                if (listData[insertPosition].Price == listData[insertPosition - 1].Price)
                {
                    listData[insertPosition].Group = listData[insertPosition - 1].Group;
                }
                else
                {
                    listData[insertPosition].Group = listData[insertPosition - 1].Group + 1;
                    for (int i = insertPosition + 1; i < listData.Count; i++)
                    {
                        listData[i].Group++;
                    }
                }
            }
            else
            {
                listData[insertPosition].Group = 0;
                for (int i = insertPosition + 1; i < listData.Count; i++)
                {
                    listData[i].Group++;
                }
            }
        }

        private bool IsRepeatItem(int insertPosition, ObservableCollection<L2Data> listData, L2Data l2Data)
        {
            if (insertPosition > 0)
            {
                if (l2Data.Exchange == listData[insertPosition - 1].Exchange && l2Data.Price == listData[insertPosition - 1].Price)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 寻找L2插入的位置
        /// </summary>
        /// <param name="l2Data">插入的数据</param>
        /// <param name="listData">插入的列表</param>
        /// <param name="side">列表的买卖方向</param>
        /// <returns></returns>
        private int FindIndex(L2Data l2Data, ObservableCollection<L2Data> listData, TradeDirection side)
        {
            var low = 0;
            var height = listData.Count - 1;
            int insertPosition = 0;

            while (low <= height)
            {
                insertPosition = (low + height) / 2;
                if (side == TradeDirection.Buy)
                {
                    if (l2Data.Price > listData[insertPosition].Price)
                    {
                        height = insertPosition - 1;
                    }
                    else
                    {
                        low = insertPosition + 1;
                    }
                }
                else if (side == TradeDirection.Sell)
                {
                    if (l2Data.Price < listData[insertPosition].Price)
                    {
                        height = insertPosition - 1;
                    }
                    else
                    {
                        low = insertPosition + 1;
                    }
                }
            }

            insertPosition++;
            return insertPosition;
        }

        /// <summary>
        /// 接收L2数据查询数据
        /// </summary>
        /// <param name="event">L2数据查询事件</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Handle(L2DataQueryEvent @event, CancellationToken token)
        {
            //收到消息不为当前订阅则不处理
            if (@event.Symbol != TosVM.Symbol)
                return;

            _viewHeader = @event.Symbol.Split("-");
            //注意：操作UI控件的数据源需要UI所在线程处理
            await Dispatcher.InvokeAsync(() =>
            {
                //插入精度
                InsertPrecision(@event.Precision);

                Title = $"行情:{@event.Symbol.ToUpper()}";
                TosVM.BuyListData.Clear();
                TosVM.SellListData.Clear();

                foreach (var item in @event.BuyTradeItems)
                {
                    var L2Data = _mapper.MapToL2(item, @event.Precision);
                    ListInsert(L2Data, TradeDirection.Buy);
                }

                foreach (var item in @event.SellTradeItems)
                {
                    var L2Data = _mapper.MapToL2(item, @event.Precision);
                    ListInsert(L2Data, TradeDirection.Sell);
                }

                BindingPropertyChange();
            });
        }

        /// <summary>
        /// 插入精度列表
        /// </summary>
        /// <param name="precision">默认精度</param>
        private void InsertPrecision(int precision)
        {
            TosVM.MaxPrecision = precision;
            TosVM.Precision.Clear();
            for (int i = 0; i < 7; i++)
            {
                if (precision - i >= 0)
                {
                    TosVM.Precision.Insert(TosVM.Precision.Count, _mapper.MapToPrecisionString(precision - i));
                }
            }
            TosVM.SelectPrecision = _mapper.MapToPrecisionString(precision);
        }

        /// <summary>
        /// 接收TOS查询数据
        /// </summary>
        /// <param name="event"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Handle(TOSDataQueryEvent @event, CancellationToken token)
        {
            //收到的信息不为当前订阅则不处理   
            if (@event.Symbol != _tosSubjectObserver.Symbol)
                return;

            await Dispatcher.InvokeAsync(() =>
            {
                TosVM.ListData.Clear();
                foreach (var item in @event.TOSItems)
                {
                    var TOSData = _mapper.MapToTOS(item);
                    RemoveListItem(TosVM.ListData);
                    TosVM.ListData.Insert(0, TOSData);
                }
            });
        }

        public void LoadeTemplate()
        {
            _windowCommonBehavior.LoadeTemplate();

            Title = TosVM.TosTitle;
        }

        public void Subscribe(IEventBusRegistrator registrator)
        {
            registrator.Register<TOSDataIncomingEvent>(this);
            registrator.Register<L2DataIncomingEvent>(this);
            registrator.Register<L2DataQueryEvent>(this);
            registrator.Register<TOSDataQueryEvent>(this);
            registrator.Register<PrecisionChangeSubscribeEvent>(this);
            registrator.Register<PrecisionChangeUnsubscribeEvent>(this);
        }

        /// <summary>
        /// 改变精度后订阅
        /// </summary>
        /// <param name="event"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Handle(PrecisionChangeSubscribeEvent @event, CancellationToken token)
        {
            if (@event.Symbol == TosVM.Symbol)
            {
                if (_l2SubjectObserver != null)
                {
                    await _l2SubjectObserver.Unsubscribe();
                    TosVM.NowPrecision = @event.Precision;
                    TosVM.SelectPrecision = _mapper.MapToPrecisionString(TosVM.MaxPrecision - @event.Precision);
                }
            }
        }

        /// <summary>
        /// 改变精度后取消旧订阅
        /// </summary>
        /// <param name="event"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Handle(PrecisionChangeUnsubscribeEvent @event, CancellationToken token)
        {
            if (@event.Symbol == TosVM.Symbol)
            {
                _l2SubjectObserver = await _tosService.SubscribeL2Data(TosVM.Symbol, this, @event.Precision);
            }
        }
    }
}
