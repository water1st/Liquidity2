using System.ComponentModel;

namespace Liquidity2.UI.Present.Windows.Asset
{
    public class AssetData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Symbol { get; }

        private string _available;

        /// <summary>
        /// 可用
        /// </summary>
        public string Available
        {
            get => _available;
            set
            {
                _available = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Available)));
            }
        }

        /// <summary>
        /// 冻结
        /// </summary>
        private string _frozen;
        public string Frozen
        {
            get => _frozen;
            set
            {
                _frozen = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Frozen)));
            }
        }

        public AssetData(string available, string frozen, string symbol)
        {
            Available = available;
            Frozen = frozen;
            Symbol = symbol;
        }
    }
}
