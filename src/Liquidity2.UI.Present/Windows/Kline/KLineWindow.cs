using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Components.Chart;
using Liquidity2.UI.Components.Chart.Render;
using Liquidity2.UI.Components.KLine.Interface;
using Liquidity2.UI.Components.KLine.Model;
using Liquidity2.UI.Components.KLine.Renderer;
using Liquidity2.UI.Components.Renderer;
using Liquidity2.UI.Components.UsersControl;
using Liquidity2.UI.Components.Volume.Interface;
using Liquidity2.UI.Components.Volume.Renderer;
using Liquidity2.UI.Core;
using Liquidity2.UI.Windows.TOS.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Liquidity2.UI.Present.Windows.Kline
{
    public class KLineWindow : WindowBase
    //IEventHandler<OneMinuteKLineDataIncomingEvent>,
    //IEventHandler<DigitalCurrencyKLineQueryToUIEvent>,
    //IEventHandler<DigitalCurrencyCandleDataToUIEvent>,
    //IEventHandler<DigitalCurrencyL2QueryEvent>,
    //IEventHandler<GroupSubscribeEvent>,
    //IEventHandler<SelfSelectSearchEvent>,
    //IEventHandler<GetMarkSymbolResponseToUIEvent>
    {
        //private readonly IKLineService kLineService;
        private readonly IKLineDataMapper kLineDataMapper;
        //private readonly IDigitalCurrencyMarketService marketService;
        public KLineViewModel KLineVM { get; }
        //private IFutureMarketObserver observer;
        private readonly IEventBus bus;
        //private readonly ISelfSelectService selfSelectService;

        public ICommand DropdownCommand { get; set; }
        public ICommand MoveLeftCommand { get; set; }
        public ICommand MoveRightCommand { get; set; }
        public ICommand ZoomInCommand { get; set; }
        public ICommand ZoomOutCommand { get; set; }
        public ICommand MouseMoveCommand { get; set; }

        public TimeMenuViewModel TimeMenuVM { get; set; }
        public IChartProxy<IKLineChart> KlineChartProxy { get; set; }
        public IChartProxy<IVolumeChart> VolumeChartProxy { get; set; }

        public KLineWindow(IWindowCommonBehavior windowCommonBehavior, /*IKLineService kLineService, */IEventBus bus, IChartProxy<IKLineChart> klineChartProxy, IChartProxy<IVolumeChart> volumeChartProxy, IKLineDataMapper kLineDataMapper/*, IDigitalCurrencyMarketService marketService, ISelfSelectService selfSelectService*/) : base(windowCommonBehavior)
        {
            //this.kLineService = kLineService;
            this.kLineDataMapper = kLineDataMapper;
            //this.marketService = marketService;
            //this.selfSelectService = selfSelectService;
            //this.bus = bus;
            KLineVM = new KLineViewModel
            {
                Group = "/",
                KLineTypeface = new Typeface("Verdana"),
                KlineTimeSpan = "1m",
                KlineTitle = "k线"
            };
            SelectKLineRender();
            TimeMenuVM = new TimeMenuViewModel();
            this.bus = bus;
            KlineChartProxy = klineChartProxy;
            VolumeChartProxy = volumeChartProxy;
            RegisterEvent(this.bus);
            InitializeCommand();
            //加入Group按钮
            HasGroup = true;
            //加入Search按钮
            HasSearch = true;

            windowCommonBehavior.WindowSearchChanged += WindowSearchChanged;
            windowCommonBehavior.WindowGroupChanged += WindowGroupChanged;

            BindingCommand();

            DataContext = this;
            Title = KLineVM.KlineTitle;
        }

        private void BindingCommand()
        {
            this.AddCommandBinding(KLineVM.StarButtonClickCmd, CanExecute, StarButtonClick_Executed);
        }

        private void StarButtonClick_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.OriginalSource is StarButton button)
            {
                if (button.StarIsSelfSelect)
                {
                    //selfSelectService.RemoveMarkSymbol(KLineVM.Symbol);
                }
                else
                {
                    //selfSelectService.AddMarkSymbol(KLineVM.Symbol);
                }

                button.StarIsSelfSelect = !button.StarIsSelfSelect;
            }
        }

        private void WindowGroupChanged(object sender, PropertyChangedEventArgs e)
        {
            GroupActivate = e.PropertyName != "/";
            KLineVM.Group = e.PropertyName;
        }

        private void WindowSearchChanged(object sender, PropertyChangedEventArgs e)
        {
            KLineVM.Symbol = e.PropertyName;

            SubscribeData().ContinueWith(task =>
            {
                // 通知其他同组窗口更变订阅
                if (GroupActivate && !string.IsNullOrEmpty(KLineVM.Symbol))
                {
                    //bus.Push(new GroupSubscribeEvent(WindowId, KLineVM.Symbol, KLineVM.Group));
                }
            });
        }

        /// <summary>
        /// 查询和订阅K线
        /// </summary>
        private async Task SubscribeData()
        {
            if (KLineVM.Symbol is null)
            {
                return;
            }

            //await selfSelectService.GetMarkSymbol();
            //await marketService.GetL2Data(KLineVM.Symbol);
            //await kLineService.SubscribeCandlesByTime(KLineVM.Symbol, KLineVM.KlineTimeSpan);
        }

        private void RegisterEvent(IEventBus bus)
        {
            //bus.RegisterEventHandler<DigitalCurrencyKLineQueryToUIEvent>(this);
            //bus.RegisterEventHandler<DigitalCurrencyCandleDataToUIEvent>(this);
            //bus.RegisterEventHandler<SelfSelectSearchEvent>(this);
            //bus.RegisterEventHandler<DigitalCurrencyL2QueryEvent>(this);
            //bus.RegisterEventHandler<GroupSubscribeEvent>(this);
            //bus.RegisterEventHandler<GetMarkSymbolResponseToUIEvent>(this);
        }

        private void InitializeCommand()
        {
            DropdownCommand = new CustomRoutedCommand(nameof(DropdownCommand), typeof(KLineWindow), this);
            this.AddCommandBinding(DropdownCommand, CanExecute, DropdownCommandcb_Executed);

            MoveLeftCommand = new CustomRoutedCommand(nameof(MoveLeftCommand), typeof(KLineWindow), this);
            this.AddCommandBinding(MoveLeftCommand, CanExecute, MoveLeftCommandcb_Executed);

            MoveRightCommand = new CustomRoutedCommand(nameof(MoveRightCommand), typeof(KLineWindow), this);
            this.AddCommandBinding(MoveRightCommand, CanExecute, MoveRightCommandcb_Executed);

            ZoomInCommand = new CustomRoutedCommand(nameof(ZoomInCommand), typeof(KLineWindow), this);
            this.AddCommandBinding(ZoomInCommand, CanExecute, ZoomInCommandcb_Executed);

            ZoomOutCommand = new CustomRoutedCommand(nameof(ZoomOutCommand), typeof(KLineWindow), this);
            this.AddCommandBinding(ZoomOutCommand, CanExecute, ZoomOutCommandcb_Executed);

            MouseMoveCommand = new CustomRoutedCommand(nameof(MouseMoveCommand), typeof(KLineWindow), this);
            this.AddCommandBinding(MouseMoveCommand, CanExecute, MouseMoveCommandcb_Executed);
        }

        private void MouseMoveCommandcb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            KlineChartProxy.Chart.LinkageMove();
            VolumeChartProxy.Chart.LinkageMove();
        }

        private void ZoomOutCommandcb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            KlineChartProxy.Chart.LinkageZoomOut();
            VolumeChartProxy.Chart.LinkageZoomOut();
        }

        private void ZoomInCommandcb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            KlineChartProxy.Chart.LinkageZoomIn();
            VolumeChartProxy.Chart.LinkageZoomIn();
        }

        private void MoveRightCommandcb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            KlineChartProxy.Chart.LinkageMoveRight();
            VolumeChartProxy.Chart.LinkageMoveRight();
        }

        private void MoveLeftCommandcb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            KlineChartProxy.Chart.LinkageMoveLeft();
            VolumeChartProxy.Chart.LinkageMoveLeft();
        }

        private void DropdownCommandcb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var timeSpan = kLineDataMapper.MapToTimeSpan(e.Parameter.ToString());

            //暂时只聚合到分钟,和一天的数据
            if ((int)timeSpan <= 5 || (int)timeSpan == 8)
            {
                KLineVM.KlineTimeSpan = e.Parameter.ToString();
                KlineChartProxy.Chart.TimeSpan = timeSpan;
                VolumeChartProxy.Chart.TimeSpan = timeSpan;
            }

            SubscribeData().ContinueWith(task =>
            { });
        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        //public Task Handle(OneMinuteKLineDataIncomingEvent @event)
        //{
        //    return Task.CompletedTask;
        //}

        //protected override void OnClosed(EventArgs e)
        //{
        //    observer?.Unsubscribe();
        //    base.OnClosed(e);
        //}

        //public async Task Handle(DigitalCurrencyKLineQueryToUIEvent @event)
        //{
        //    //收到的信息不为当前订阅则不处理   
        //    if (@event.Symbol != KLineVM.Symbol)
        //        return;
        //    //收到消息不为当前TimeSpan则不处理
        //    if ((int)@event.KlineTimeSpan != (int)kLineDataMapper.MapToTimeSpan(KLineVM.KlineTimeSpan))
        //        return;

        //    KLineVM.SymbolText = @event.Symbol.Replace("-", "/").ToUpper();

        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        Title = $"K线:{@event.Symbol.ToUpper()}";
        //        KlineChartProxy.Chart.OHLCs = new List<OHLC>();
        //        KlineChartProxy.Chart.Lines = new List<decimal>();
        //        VolumeChartProxy.Chart.VolumeItems = new List<VolumeItem>();

        //        KLineVM.Volume = 0;
        //        foreach (var item in @event.KLineItems)
        //        {
        //            KlineChartProxy.Chart.OHLCs.Add(new OHLC
        //            {
        //                Close = item.Close,
        //                Height = item.High,
        //                Low = item.Low,
        //                Open = item.Open,
        //                Volume = item.Volume,
        //                Price = item.Turnover,
        //                Time = DateTimeOffset.FromUnixTimeSeconds(item.UnixTimeStamp).DateTime.ToLocalTime()
        //            });
        //            KlineChartProxy.Chart.Lines.Add(item.Close);

        //            VolumeChartProxy.Chart.VolumeItems.Add(new VolumeItem { Close = item.Close, Open = item.Open, Time = DateTimeOffset.FromUnixTimeSeconds(item.UnixTimeStamp).DateTime.ToLocalTime(), Volume = item.Volume });

        //            KLineVM.Volume += item.Volume;
        //        }

        //        var timeSpan = kLineDataMapper.MapToTimeSpan(KLineVM.KlineTimeSpan);
        //        if (timeSpan > 0)
        //        {
        //            KlineChartProxy.Chart.Lines = @event.AvgLineItems.ToList();
        //        }

        //        KlineChartProxy.Chart.DrawNewest();
        //        VolumeChartProxy.Chart.DrawNewest();
        //    });
        //}

        private void SelectKLineRender()
        {
            var kLineRender = new List<Type>
            {
                typeof(KLineRenderer),
                typeof(KlineInfoRender),
                typeof(AxisLabelRenderer),
                typeof(AxisLineRenderer),
                typeof(LineRender),
                typeof(GuideLineRenderer)
            };

            var volumeRender = new List<Type>
            {
                typeof(VolumeRenderer),
                typeof (AxisLabelRenderer),
                typeof(AxisLineRenderer),
                typeof(LineRender),
                typeof(TimeLabelRenderer),
                typeof(GuideLineRenderer)
            };

            KLineVM.KLineSelcetRenders = kLineRender;
            KLineVM.VolumeSelcetRenders = volumeRender;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            KlineChartProxy.Chart.Draw();
            VolumeChartProxy.Chart.Draw();
            base.OnRenderSizeChanged(sizeInfo);
        }

        //public async Task Handle(DigitalCurrencyCandleDataToUIEvent @event)
        //{
        //    //收到的信息不为当前订阅则不处理   
        //    if (@event.Symbol != KLineVM.Symbol)
        //        return;

        //    var timeSpan = kLineDataMapper.MapToTimeSpan(KLineVM.KlineTimeSpan);
        //    if ((int)timeSpan > 1)
        //        return;
        //    var ohlc = new OHLC
        //    {
        //        Close = @event.CandleItem.Close,
        //        Height = @event.CandleItem.High,
        //        Low = @event.CandleItem.Low,
        //        Open = @event.CandleItem.Open,
        //        Price = @event.CandleItem.Turnover,
        //        Volume = @event.CandleItem.Volume,
        //        Time = DateTimeOffset.FromUnixTimeSeconds(@event.CandleItem.UnixTimeStamp).DateTime.ToLocalTime()
        //    };

        //    var volume = new VolumeItem
        //    {
        //        Close = @event.CandleItem.Close,
        //        Open = @event.CandleItem.Open,
        //        Time = DateTimeOffset.FromUnixTimeSeconds(@event.CandleItem.UnixTimeStamp).DateTime.ToLocalTime(),
        //        Volume = @event.CandleItem.Volume
        //    };

        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        if (KlineChartProxy.Chart.OHLCs == null)
        //            return;

        //        var avgLintPoint = ComputeAvgLintPoint(KlineChartProxy.Chart.OHLCs, ohlc.Close);

        //        var index = KlineChartProxy.Chart.OHLCs.ToList().FindIndex(x => x.Time == ohlc.Time);

        //        if (index != -1)
        //        {
        //            KlineChartProxy.Chart.OHLCs.RemoveAt(index);
        //            KlineChartProxy.Chart.Lines.RemoveAt(index);

        //            VolumeChartProxy.Chart.VolumeItems.RemoveAt(index);
        //        }

        //        KlineChartProxy.Chart.OHLCs.Add(ohlc);
        //        KlineChartProxy.Chart.Lines.Add(avgLintPoint);
        //        VolumeChartProxy.Chart.VolumeItems.Add(volume);
        //        KlineChartProxy.Chart.Draw();
        //        VolumeChartProxy.Chart.Draw();
        //    });

        //    //绑定数据
        //    BindingPropertyChange(ohlc, @event.CandleItem.Volume);
        //}

        /// <summary>
        /// 计算推送过来的均线点
        /// </summary>
        /// <param name="oHLCs"></param>
        /// <param name="close"></param>
        /// <returns></returns>
        private decimal ComputeAvgLintPoint(IList<OHLC> oHLCs, decimal close)
        {
            decimal avgPoint = close;
            for (int i = oHLCs.Count - 5; i < oHLCs.Count; i++)
            {
                avgPoint += oHLCs[i].Close;
            }

            return avgPoint / 6;
        }

        private void BindingPropertyChange(OHLC ohlc, decimal volume)
        {
            KLineVM.ClosePrice = ohlc.Close;
            KLineVM.HighPrice = ohlc.Height;
            var amountOfIncrease = (ohlc.Close - ohlc.Open) / ohlc.Open;
            if (amountOfIncrease > 0)
            {
                KLineVM.AmountOfIncrease = "+" + amountOfIncrease.ToString("P");
            }
            else
            {
                KLineVM.AmountOfIncrease = amountOfIncrease.ToString("P");
            }
        }

        //public async Task Handle(SelfSelectSearchEvent @event)
        //{
        //    if (@event.Group == KLineVM.Group)
        //    {
        //        KLineVM.Symbol = @event.Symbol;
        //        await SubscribeData();
        //    }
        //}

        //public async Task Handle(DigitalCurrencyL2QueryEvent @event)
        //{
        //    if (@event.Symbol == KLineVM.Symbol)
        //    {
        //        await Dispatcher.InvokeAsync(() =>
        //        {
        //            KLineVM.TextFormat = $"N{@event.Precision}";
        //        });
        //    }
        //}

        public async Task Handle(GroupSubscribeEvent @event)
        {
            if (@event.Group == KLineVM.Group)
            {
                KLineVM.Symbol = @event.Symbol;
                await SubscribeData();
            }
        }

        //获取自选列表，判断是否是自选币
        //public async Task Handle(GetMarkSymbolResponseToUIEvent @event)
        //{
        //    var index = @event.MarkSymbols.ToList().FindIndex(x => x == KLineVM.Symbol);

        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        if (index != -1)
        //        {
        //            KLineVM.IsSelfSelect = true;
        //        }
        //        else
        //        {
        //            KLineVM.IsSelfSelect = false;
        //        }
        //    });
        //}
    }
}
