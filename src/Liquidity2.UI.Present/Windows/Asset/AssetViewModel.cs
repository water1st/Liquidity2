using System.Collections.ObjectModel;

namespace Liquidity2.UI.Present.Windows.Asset
{
    public class AssetViewModel
    {
        public ObservableCollection<AssetData> FundAccountData { get; set; } = new ObservableCollection<AssetData>();

        //public ObservableCollection<WalletData> TransactionAccountData { get; set; } = new ObservableCollection<WalletData>();
    }
}
