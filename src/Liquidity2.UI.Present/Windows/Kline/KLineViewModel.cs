using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace Liquidity2.UI.Present.Windows.Kline
{
    public class KLineViewModel : INotifyPropertyChanged
    {
        private string symbolText;

        /// <summary>
        /// 币对号码
        /// </summary>
        public string SymbolText
        {
            get => symbolText;
            set
            {
                symbolText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SymbolText)));
            }
        }

        public string KlineTimeSpan { get; set; }

        private string group;
        /// <summary>
        /// 分组
        /// </summary>
        public string Group
        {
            get => group;
            set
            {
                group = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Group)));
            }
        }

        private decimal highPrice;

        /// <summary>
        /// 最高价
        /// </summary>
        public decimal HighPrice
        {
            get => highPrice;
            set
            {
                highPrice = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HighPrice)));
            }
        }


        private string textFormat;

        public string TextFormat
        {
            get => textFormat;
            set
            {
                textFormat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextFormat)));
            }
        }

        private string klineTitle;

        public string KlineTitle
        {
            get => klineTitle;
            set
            {
                klineTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(KlineTitle)));
            }
        }


        private decimal closePrice;
        /// <summary>
        /// 最新价
        /// </summary>
        public decimal ClosePrice
        {
            get => closePrice;
            set
            {
                closePrice = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ClosePrice)));
            }
        }

        private decimal volume;

        /// <summary>
        /// 成交量
        /// </summary>
        public decimal Volume
        {
            get => volume;
            set
            {
                volume = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Volume)));
            }
        }

        private string amountOfIncrease;
        /// <summary>
        /// 涨幅
        /// </summary>

        public string AmountOfIncrease
        {
            get => amountOfIncrease;
            set
            {
                amountOfIncrease = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AmountOfIncrease)));
            }
        }

        private string symbol;
        public string Symbol
        {
            get => symbol;
            set
            {
                symbol = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Symbol)));
            }
        }

        private Typeface typeface;
        public Typeface KLineTypeface
        {
            get => typeface;
            set
            {
                typeface = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(KLineTypeface)));
            }
        }

        private IList<Type> kLineSelectRenders;
        public IList<Type> KLineSelcetRenders
        {
            get => kLineSelectRenders;
            set
            {
                kLineSelectRenders = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(KLineSelcetRenders)));
            }
        }

        private IList<Type> _volumeSelcetRenders;
        public IList<Type> VolumeSelcetRenders
        {
            get => _volumeSelcetRenders;
            set
            {
                _volumeSelcetRenders = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VolumeSelcetRenders)));
            }
        }

        private bool _isSelfSelect;
        //当前比对为自选
        public bool IsSelfSelect
        {
            get => _isSelfSelect;
            set
            {
                _isSelfSelect = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelfSelect)));
            }
        }

        public ICommand StarButtonClickCmd { get; set; } = new CustomRoutedCommand(nameof(StarButtonClickCmd), typeof(KLineViewModel));

        public event PropertyChangedEventHandler PropertyChanged;
        public KLineViewModel()
        {
            KLineSelcetRenders = new List<Type>();
            VolumeSelcetRenders = new List<Type>();
        }
    }
}
