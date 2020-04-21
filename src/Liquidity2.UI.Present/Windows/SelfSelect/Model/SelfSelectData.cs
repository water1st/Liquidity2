using System.ComponentModel;

namespace Liquidity2.UI.Windows.SelfSelect.Model
{
    public class SelfSelectData : INotifyPropertyChanged
    {
        public SelfSelectData(string symbol, double price, double rate)
        {
            Symbol = symbol;
            NewestPrice = price.ToString("0.########");
            IncreaseRate = rate;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _symbol;

        public string Symbol
        {
            get => _symbol;
            set
            {
                _symbol = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Symbol)));
            }
        }

        private string _newestPrice;

        public string NewestPrice
        {
            get => _newestPrice;
            set
            {
                _newestPrice = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewestPrice)));
            }
        }

        private double _increaseRate;

        public double IncreaseRate
        {
            get => _increaseRate;
            set
            {
                _increaseRate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IncreaseRate)));
            }
        }

        private bool _isSelfSelect;

        public bool IsSelfSelect
        {
            get => _isSelfSelect;
            set
            {
                _isSelfSelect = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelfSelect)));
            }
        }

    }
}
