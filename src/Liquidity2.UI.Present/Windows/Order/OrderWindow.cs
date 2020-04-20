using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Core;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Liquidity2.UI.Windows
{
    /// <summary>
    /// OrderWindow_Template.xaml 的交互逻辑
    /// </summary>
    public class OrderWindow : WindowBase
    //IEventHandler<GetWalletsQueryToUIEvent>,
    //IEventHandler<CreateBuyOrderEvent>,
    //IEventHandler<CreateSellOrderEvent>
    {
        //private readonly IOrderService _orderService;
        //private readonly IAccountService _accountService;
        private readonly IEventBus _bus;

        public LimitOrderViewModel LimitOrderViewModel { get; set; } = new LimitOrderViewModel();

        public OrderWindow(IWindowCommonBehavior windowCommonBehavior, IEventBus bus/*, IOrderService orderService, IAccountService accountService*/) : base(windowCommonBehavior)
        {
            //_orderService = orderService;
            _bus = bus;
            //_accountService = accountService;

            RegisterHandler(_bus);
            BingdingCommand();
        }

        //初始化下单进度条里的点
        private void BingdingCommand()
        {
            this.AddCommandBinding(LimitOrderViewModel.BuyingSliderDot1ClickCmd, Buying_Slider_Dot_Click_CanExecute, Buying_Slider_Dot1_Click_Executed);
            this.AddCommandBinding(LimitOrderViewModel.BuyingSliderDot2ClickCmd, Buying_Slider_Dot_Click_CanExecute, Buying_Slider_Dot2_Click_Executed);
            this.AddCommandBinding(LimitOrderViewModel.BuyingSliderDot3ClickCmd, Buying_Slider_Dot_Click_CanExecute, Buying_Slider_Dot3_Click_Executed);
            this.AddCommandBinding(LimitOrderViewModel.BuyingSliderDot4ClickCmd, Buying_Slider_Dot_Click_CanExecute, Buying_Slider_Dot4_Click_Executed);
            this.AddCommandBinding(LimitOrderViewModel.BuyingSliderDot5ClickCmd, Buying_Slider_Dot_Click_CanExecute, Buying_Slider_Dot5_Click_Executed);
            this.AddCommandBinding(LimitOrderViewModel.SellingSliderDot1ClickCmd, Selling_Slider_Dot_Click_CanExecute, Selling_Slider_Dot1_Click_Executed);
            this.AddCommandBinding(LimitOrderViewModel.SellingSliderDot2ClickCmd, Selling_Slider_Dot_Click_CanExecute, Selling_Slider_Dot2_Click_Executed);
            this.AddCommandBinding(LimitOrderViewModel.SellingSliderDot3ClickCmd, Selling_Slider_Dot_Click_CanExecute, Selling_Slider_Dot3_Click_Executed);
            this.AddCommandBinding(LimitOrderViewModel.SellingSliderDot4ClickCmd, Selling_Slider_Dot_Click_CanExecute, Selling_Slider_Dot4_Click_Executed);
            this.AddCommandBinding(LimitOrderViewModel.SellingSliderDot5ClickCmd, Selling_Slider_Dot_Click_CanExecute, Selling_Slider_Dot5_Click_Executed);
            this.AddCommandBinding(LimitOrderViewModel.LimitBuyButtonClickCmd, Limit_Buy_Button_Click_CanExecute, Limit_Buy_Button_Click_Executed);
            this.AddCommandBinding(LimitOrderViewModel.LimitSellButtonClickCmd, Limit_Sell_Button_Click_CanExecute, Limit_Sell_Button_Click_Executed);
        }

        private void RegisterHandler(IEventBus bus)
        {
            //bus.RegisterEventHandler<GetWalletsQueryToUIEvent>(this);
            //bus.RegisterEventHandler<CreateBuyOrderEvent>(this);
            //bus.RegisterEventHandler<CreateSellOrderEvent>(this);
        }

        //下单数量进度条的点
        private void Buying_Slider_Dot_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Buying_Slider_Dot1_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LimitOrderViewModel.BuyingVolume = 0;
            e.Handled = true;
        }

        private void Buying_Slider_Dot2_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LimitOrderViewModel.BuyingVolume = LimitOrderViewModel.BuyingAvailable * 0.25M;
            e.Handled = true;
        }

        private void Buying_Slider_Dot3_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LimitOrderViewModel.BuyingVolume = LimitOrderViewModel.BuyingAvailable * 0.5M;
            e.Handled = true;
        }

        private void Buying_Slider_Dot4_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LimitOrderViewModel.BuyingVolume = LimitOrderViewModel.BuyingAvailable * 0.75M;
            e.Handled = true;
        }

        private void Buying_Slider_Dot5_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LimitOrderViewModel.BuyingVolume = LimitOrderViewModel.BuyingAvailable;
            e.Handled = true;
        }

        private void Selling_Slider_Dot_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Selling_Slider_Dot1_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LimitOrderViewModel.SellingVolume = 0;
            e.Handled = true;
        }

        private void Selling_Slider_Dot2_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LimitOrderViewModel.SellingVolume = LimitOrderViewModel.SellingAvailable * 0.25M;
            e.Handled = true;
        }

        private void Selling_Slider_Dot3_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LimitOrderViewModel.SellingVolume = LimitOrderViewModel.SellingAvailable * 0.5M;
            e.Handled = true;
        }

        private void Selling_Slider_Dot4_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LimitOrderViewModel.SellingVolume = LimitOrderViewModel.SellingAvailable * 0.75M;
            e.Handled = true;
        }

        private void Selling_Slider_Dot5_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LimitOrderViewModel.SellingVolume = LimitOrderViewModel.SellingAvailable;
            e.Handled = true;
        }

        private void Limit_Buy_Button_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Limit_Buy_Button_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (LimitOrderViewModel.BuyingPrice == 0)
            {
                LimitOrderViewModel.BuyErrorMessage = "买入价格不能为0";
                return;
            }
            if (LimitOrderViewModel.BuyingAmount == 0)
            {
                LimitOrderViewModel.BuyErrorMessage = "买入量不能为0";
                return;
            }

            LimitOrderViewModel.BuyErrorMessage = "";
            //_orderService.PlaceOrder(OrderSide.LimitBuy, LimitOrderViewModel.ToCurrency + "-" + LimitOrderViewModel.FromCurrency, (double)LimitOrderViewModel.BuyingPrice, (double)LimitOrderViewModel.BuyingAmount, LimitOrderViewModel.BuyingExchange);

            //_accountService.GetWallets();

            LimitOrderViewModel.BuyingPriceString = "";
            LimitOrderViewModel.BuyingPrice = 0;
            LimitOrderViewModel.BuyingVolume = 0;
            LimitOrderViewModel.BuyingVolumeString = "";

            e.Handled = true;
        }

        private void Limit_Sell_Button_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Limit_Sell_Button_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (LimitOrderViewModel.SellingPrice == 0)
            {
                LimitOrderViewModel.SellErrorMessage = "卖出价格不能为0";
                return;
            }
            if (LimitOrderViewModel.SellingVolume == 0)
            {
                LimitOrderViewModel.SellErrorMessage = "卖出量不能为0";
                return;
            }

            LimitOrderViewModel.SellErrorMessage = "";
            //_orderService.PlaceOrder(OrderSide.LimitSell, LimitOrderViewModel.ToCurrency + "-" + LimitOrderViewModel.FromCurrency, (double)LimitOrderViewModel.SellingPrice, (double)LimitOrderViewModel.SellingVolume, LimitOrderViewModel.SellingExchange);

            //_accountService.GetWallets();

            LimitOrderViewModel.SellingPriceString = "";
            LimitOrderViewModel.SellingPrice = 0;
            LimitOrderViewModel.SellingVolume = 0;
            LimitOrderViewModel.SellingVolumeString = "";

            e.Handled = true;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.OriginalSource is TextBox textBox && e.Key == Key.Decimal)
            {
                if (textBox.Text.Contains("."))
                {
                    e.Handled = true; //不可重复输入.
                }
            }

            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.Decimal || e.Key == Key.OemPeriod || e.Key == Key.Left || e.Key == Key.Right) || Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                e.Handled = true;//不可输入
            }
        }

        //public async Task Handle(CreateBuyOrderEvent @event)
        //{
        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        LimitOrderViewModel.FromCurrency = @event.FromCurrency.ToUpper();
        //        LimitOrderViewModel.ToCurrency = @event.ToCurrency.ToUpper();
        //        LimitOrderViewModel.BuyingPriceString = @event.Price.ToString();
        //        LimitOrderViewModel.BuyingExchange = @event.Exchange;
        //        LimitOrderViewModel.SellingPriceString = @event.Price.ToString();
        //        LimitOrderViewModel.SellingExchange = @event.Exchange;
        //        Title = $"下单:{LimitOrderViewModel.ToCurrency}-{LimitOrderViewModel.FromCurrency}";
        //    });

        //    await _accountService.GetWallets();
        //    this.Activate();
        //}

        //public async Task Handle(CreateSellOrderEvent @event)
        //{
        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        LimitOrderViewModel.FromCurrency = @event.FromCurrency.ToUpper();
        //        LimitOrderViewModel.ToCurrency = @event.ToCurrency.ToUpper();
        //        LimitOrderViewModel.SellingPriceString = @event.Price.ToString();
        //        LimitOrderViewModel.SellingExchange = @event.Exchange;
        //        LimitOrderViewModel.BuyingPriceString = @event.Price.ToString();
        //        LimitOrderViewModel.BuyingExchange = @event.Exchange;
        //        Title = $"下单:{LimitOrderViewModel.ToCurrency}-{LimitOrderViewModel.FromCurrency}";
        //    });

        //    await _accountService.GetWallets();
        //    this.Activate();
        //}

        //public async Task Handle(GetWalletsQueryToUIEvent @event)
        //{
        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        if (@event.WalletDatas.Any(p => p.Currency == LimitOrderViewModel.FromCurrency.ToLower()))
        //        {
        //            var BuyingAvailable = @event.WalletDatas.Select((item) => new
        //            { Item = item }).First(i => i.Item.Currency == LimitOrderViewModel.FromCurrency.ToLower()).Item.Available;
        //            LimitOrderViewModel.BuyingAvailable = Convert.ToDecimal(BuyingAvailable);
        //        }
        //        else
        //        {
        //            LimitOrderViewModel.BuyingAvailable = 0;
        //        }

        //        if (@event.WalletDatas.Any(p => p.Currency == LimitOrderViewModel.ToCurrency.ToLower()))
        //        {
        //            var SellingAvailable = @event.WalletDatas.Select((item) => new
        //            { Item = item }).FirstOrDefault(i => i.Item.Currency == LimitOrderViewModel.ToCurrency.ToLower()).Item.Available;
        //            LimitOrderViewModel.SellingAvailable = Convert.ToDecimal(SellingAvailable);
        //        }
        //        else
        //        {
        //            LimitOrderViewModel.SellingAvailable = 0;
        //        }
        //    });
        //}

        protected override void OnClosing(CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}