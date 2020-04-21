using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Components.UsersControl;
using Liquidity2.UI.Core;
using Liquidity2.UI.Services.SelfSelect;
using Liquidity2.UI.Services.SelfSelect.Events;
using Liquidity2.UI.Windows.SelfSelect.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Liquidity2.UI.Windows.SelfSelect
{
    public class SelfSelectWindow : WindowBase, IEventHandler<TickerDataIncomingEvent>, 
       IEventHandler<TickerQueryEvent>,
       IEventHandler<GetMarkSymbolResponseEvent>,
       IEventHandler<AddMarkSymbolEvent>,
       IEventHandler<RemoveMarkSymbolEvent>
    {
        private readonly IEventBus _bus;
        private readonly ISelfSelectMapper _mapper;
        private readonly ISelfSelectService _selfSelectService;

        public SelfSelectViewModel SelfSelectModel { get; } = new SelfSelectViewModel() { Group = "/" };

        public SelfSelectWindow(IWindowCommonBehavior windowCommonBehavior, IEventBus bus, ISelfSelectMapper mapper, ISelfSelectService selfSelectService) : base(windowCommonBehavior)
        {
            _bus = bus;
            _mapper = mapper;
            _selfSelectService = selfSelectService;
            HasGroup = true;
            HasSearch = false;

            RegisterHandler();
            BingdingCommand();

            windowCommonBehavior.WindowGroupChanged += WindowGroupChanged;
            SelfSelectModel.SearchTextChange += SelfSelectSearchTextBox_TextChanged;
            DataContext = this;
            InsertData();
            SelfSelectModel.NowDataSource = SelfSelectModel.SelfSelectDatas;
        }

        private void RegisterHandler()
        {
            _bus.Subscribe<TickerDataIncomingEvent>(this);
            _bus.Subscribe<TickerQueryEvent>(this);
            _bus.Subscribe<GetMarkSymbolResponseEvent>(this);
            _bus.Subscribe<AddMarkSymbolEvent>(this);
            _bus.Subscribe<RemoveMarkSymbolEvent>(this);
        }

        private void BingdingCommand()
        {
            this.AddCommandBinding(SelfSelectModel.USDTdataSourceSetCmd, CanExecute, USDT_Executed);
            this.AddCommandBinding(SelfSelectModel.HUSDTdataSourceSetCmd, CanExecute, HUSDT_Executed);
            this.AddCommandBinding(SelfSelectModel.BTCdataSourceSetCmd, CanExecute, BTC_Executed);
            this.AddCommandBinding(SelfSelectModel.ETHdataSourceSetCmd, CanExecute, ETH_Executed);
            this.AddCommandBinding(SelfSelectModel.SelfSelectdataSourceSetCmd, CanExecute, SelfSelect_Executed);
            this.AddCommandBinding(SelfSelectModel.StarButtonClickCmd, CanExecute, StarButtonClick_Executed);
            this.AddCommandBinding(SelfSelectModel.GridViewItemClickCmd, CanExecute, GridViewItemClick_Executed);
        }

        //联动分组改变
        private void WindowGroupChanged(object sender, PropertyChangedEventArgs e)
        {
            //改变groupButton
            GroupActivate = e.PropertyName != "/";
            SelfSelectModel.Group = e.PropertyName;
        }

        //点击列表执行搜索操作
        private void GridViewItemClick_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is string symbol)
            {
                Event @event = new SelfSelectSearchEvent(symbol, SelfSelectModel.Group);
                _bus.Publish(@event, CancellationToken.None);
            }
        }

        //初始化插入数据
        private void InsertData()
        {
            _selfSelectService.GetMarkSymbol();
            _selfSelectService.SubscribeTickerData();
        }

        //自选搜索文字更改
        private void SelfSelectSearchTextBox_TextChanged(string text)
        {
            if (SelfSelectModel.NowDataSource == null) return;
            if (SelfSelectModel.NowSearched == null) SelfSelectModel.NowSearched = new ObservableCollection<SelfSelectData>();
            SelfSelectModel.NowSearched.Clear();
            if (string.IsNullOrEmpty(text))
            {
                var sortSource = SelfSelectModel.NowDataSource.OrderBy(o => o.Symbol);
                foreach (var item in sortSource)
                {
                    SelfSelectModel.NowSearched.Add(item);
                }
            }
            else
            {
                foreach (var item in SelfSelectModel.NowDataSource)
                {
                    if (item.Symbol.IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1)
                        SelfSelectModel.NowSearched.Add(item);
                }
            }
        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        //星星自选按钮点击操作
        private void StarButtonClick_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.OriginalSource is StarButton button)
            {
                if (button.StarIsSelfSelect == true)
                {
                    _selfSelectService.RemoveMarkSymbol(button.StarStringContent.Replace("/", "-").ToLower());
                    int index = SelfSelectModel.SelfSelectDatas.ToList().FindIndex(x => x.Symbol == button.StarStringContent);
                    if (index != -1)
                    {
                        SelfSelectModel.SelfSelectDatas.RemoveAt(index);
                    }
                }
                else
                {
                    _selfSelectService.AddMarkSymbol(button.StarStringContent.Replace("/", "-").ToLower());
                    int index = SelfSelectModel.AllMarkSymbols.ToList().FindIndex(x => x.Symbol == button.StarStringContent);
                    if (index != -1)
                    {
                        SelfSelectModel.SelfSelectDatas.Insert(0, SelfSelectModel.AllMarkSymbols[index]);
                    }
                }
                button.StarIsSelfSelect = !button.StarIsSelfSelect;
                SelfSelectSearchTextBox_TextChanged(SelfSelectModel.SearchText);
            }
        }

        private void SelfSelect_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            InsertData();
            SelfSelectModel.NowDataSource = SelfSelectModel.SelfSelectDatas;
            SelfSelectSearchTextBox_TextChanged(SelfSelectModel.SearchText);
            e.Handled = true;
        }

        private void USDT_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            InsertData();
            SelfSelectModel.NowDataSource = SelfSelectModel.USDTs;
            SelfSelectSearchTextBox_TextChanged(SelfSelectModel.SearchText);
            e.Handled = true;
        }

        private void HUSDT_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            InsertData();
            SelfSelectModel.NowDataSource = SelfSelectModel.HUSDTs;
            SelfSelectSearchTextBox_TextChanged(SelfSelectModel.SearchText);
            e.Handled = true;
        }

        private void BTC_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            InsertData();
            SelfSelectModel.NowDataSource = SelfSelectModel.BTCs;
            SelfSelectSearchTextBox_TextChanged(SelfSelectModel.SearchText);
            e.Handled = true;
        }

        private void ETH_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            InsertData();
            SelfSelectModel.NowDataSource = SelfSelectModel.ETHs;
            SelfSelectSearchTextBox_TextChanged(SelfSelectModel.SearchText);
            e.Handled = true;
        }

        public async Task Handle(TickerDataIncomingEvent @event, CancellationToken token)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                foreach (var item in @event.Tickers)
                {
                    var temp = item.Pair.Split("-");
                    var data = _mapper.MapToSelfSelectData(item);

                    //如果插入的数据是自选数据
                    if (SelfSelectModel.SelfSelectDatas.Any(p => p.Symbol == data.Symbol))
                    {
                        data.IsSelfSelect = true;
                    }

                    InsertTickerData(SelfSelectModel.AllMarkSymbols, data);
                    switch (temp[1])
                    {
                        case nameof(TickerType.usdt):
                            InsertTickerData(SelfSelectModel.USDTs, data);
                            break;
                        case nameof(TickerType.husd):
                            InsertTickerData(SelfSelectModel.HUSDTs, data);
                            break;
                        case nameof(TickerType.btc):
                            InsertTickerData(SelfSelectModel.BTCs, data);
                            break;
                        case nameof(TickerType.eth):
                            InsertTickerData(SelfSelectModel.ETHs, data);
                            break;
                    }
                    var selfSelectData = _mapper.MapToSelfSelectData(item);
                    selfSelectData.IsSelfSelect = true;
                    InsertTickerData(SelfSelectModel.SelfSelectDatas, selfSelectData);
                }
                SelfSelectSearchTextBox_TextChanged(SelfSelectModel.SearchText);
            });
        }

        private void InsertTickerData(ObservableCollection<SelfSelectData> selfSelectDatas, SelfSelectData data)
        {
            if (selfSelectDatas.Count == 0 || !selfSelectDatas.Any(p => p.Symbol == data.Symbol))
            {
                return;
            }
            var Index = selfSelectDatas.Select((item, index) => new { Item = item, Index = index }).FirstOrDefault(i => i.Item.Symbol == data.Symbol).Index;
            selfSelectDatas[Index] = data;
        }

        public async Task Handle(TickerQueryEvent @event, CancellationToken token)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                SelfSelectModel.USDTs.Clear();
                SelfSelectModel.HUSDTs.Clear();
                SelfSelectModel.ETHs.Clear();
                SelfSelectModel.BTCs.Clear();
                foreach (var item in @event.Tickers)
                {
                    var temp = item.Pair.Split("-");
                    var insertData = _mapper.MapToSelfSelectData(item);

                    //如果插入的数据是自选数据
                    if (SelfSelectModel.SelfSelectDatas.Any(p => p.Symbol == insertData.Symbol))
                    {
                        insertData.IsSelfSelect = true;
                    }

                    SelfSelectModel.AllMarkSymbols.Insert(0, insertData);
                    switch (temp[1])
                    {
                        case nameof(TickerType.usdt):
                            SelfSelectModel.USDTs.Insert(SelfSelectModel.USDTs.Count, insertData);
                            break;
                        case nameof(TickerType.husd):
                            SelfSelectModel.HUSDTs.Insert(SelfSelectModel.HUSDTs.Count, insertData);
                            break;
                        case nameof(TickerType.btc):
                            SelfSelectModel.BTCs.Insert(SelfSelectModel.BTCs.Count, insertData);
                            break;
                        case nameof(TickerType.eth):
                            SelfSelectModel.ETHs.Insert(SelfSelectModel.ETHs.Count, insertData);
                            break;
                    }
                }

                // 加入自选数据
                SelfSelectModel.SelfSelectDatas.Clear();
                foreach (var item in SelfSelectModel.SelfSelectDatasString)
                {
                    var selfSelectData = GetSelfSelectData(item);
                    if (selfSelectData != null)
                    {
                        SelfSelectModel.SelfSelectDatas.Insert(SelfSelectModel.SelfSelectDatas.Count, selfSelectData);
                    }
                }

                SelfSelectSearchTextBox_TextChanged(SelfSelectModel.SearchText);
            });
        }

        //获得自选币对
        public async Task Handle(GetMarkSymbolResponseEvent @event, CancellationToken token)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                SelfSelectModel.SelfSelectDatasString.Clear();
                foreach (var data in @event.MarkSymbols)
                {
                    var selfSelectString = data.Replace("-", "/").ToUpper();

                    SelfSelectModel.SelfSelectDatasString.Insert(SelfSelectModel.SelfSelectDatasString.Count, selfSelectString);
                }
            });
            //获取自选后查询所有币对
            await _selfSelectService.GetAllTickers();
        }

        private SelfSelectData GetSelfSelectData(string selfSelectString)
        {
            int index = SelfSelectModel.AllMarkSymbols.ToList().FindIndex(x => x.Symbol == selfSelectString);
            if (index != -1)
            {
                var selfSelectData = SelfSelectModel.AllMarkSymbols[index];
                selfSelectData.IsSelfSelect = true;
                return selfSelectData;
            }
            else
            {
                return null;
            }
        }

        public async Task Handle(AddMarkSymbolEvent @event, CancellationToken token)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                var selfSelectData = GetSelfSelectData(@event.Symbol.Replace("-", "/").ToUpper());

                var index = SelfSelectModel.SelfSelectDatas.ToList().FindIndex(x => x.Symbol == @event.Symbol.Replace("-", "/").ToUpper());

                if (index == -1)
                {
                    SelfSelectModel.SelfSelectDatas.Insert(SelfSelectModel.SelfSelectDatas.Count, selfSelectData);
                }
                SelfSelectSearchTextBox_TextChanged(SelfSelectModel.SearchText);
            });
        }

        public async Task Handle(RemoveMarkSymbolEvent @event, CancellationToken token)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                var selfSelectData = GetSelfSelectData(@event.Symbol.Replace("-", "/").ToUpper());
                var index = SelfSelectModel.SelfSelectDatas.ToList().FindIndex(x => x.Symbol == @event.Symbol.Replace("-", "/").ToUpper());
                if (index != -1)
                {
                    SelfSelectModel.SelfSelectDatas.RemoveAt(index);
                }
                selfSelectData.IsSelfSelect = false;
                SelfSelectSearchTextBox_TextChanged(SelfSelectModel.SearchText);
            });
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
