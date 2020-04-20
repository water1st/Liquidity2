using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Core;
using System.ComponentModel;

namespace Liquidity2.UI.Present.Windows.Asset
{
    /// <summary>
    /// AssetWindow_Template.xaml 的交互逻辑
    /// </summary>
    class AssetWindow : WindowBase/*, IEventHandler<GetAcctCashListToUIEvent>, IEventHandler<GetWalletsQueryToUIEvent>*/
    {
        private readonly IAssetDataMapper _assetDataMapper;
        //private readonly IUserFundsService _userFundsService;
        private readonly IEventBus _bus;

        public AssetViewModel AssetViewModel { get; set; } = new AssetViewModel();

        public AssetWindow(IWindowCommonBehavior windowCommonBehavior, IAssetDataMapper assetDataMapper, IEventBus bus/*, IUserFundsService userFundsService*/) : base(windowCommonBehavior)
        {
            _assetDataMapper = assetDataMapper;
            //_userFundsService = userFundsService;

            _bus = bus;
            //_bus.RegisterEventHandler<GetAcctCashListToUIEvent>(this);
            //_bus.RegisterEventHandler<GetWalletsQueryToUIEvent>(this);
            InsertData();
        }

        private void InsertData()
        {
            //_userFundsService.GetWallets();
            //_userFundsService.GetAcctCashList();
        }

        //public async Task Handle(GetAcctCashListToUIEvent @event)
        //{
        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        AssetViewModel.FundAccountData.Clear();
        //        foreach (var item in @event.AcctCashDatas)
        //        {
        //            var assetData = _assetDataMapper.MapToAssetData(item);
        //            AssetViewModel.FundAccountData.Insert(0, assetData);
        //        }
        //    });
        //}

        //public async Task Handle(GetWalletsQueryToUIEvent @event)
        //{
        //    await Dispatcher.InvokeAsync(() =>
        //    {
        //        AssetViewModel.TransactionAccountData.Clear();
        //        foreach (var item in @event.WalletDatas)
        //        {
        //            var accountData = _assetDataMapper.MapToWalletData(item);
        //            accountData.Currency = accountData.Currency.ToUpper();
        //            AssetViewModel.TransactionAccountData.Insert(AssetViewModel.TransactionAccountData.Count, accountData);
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
