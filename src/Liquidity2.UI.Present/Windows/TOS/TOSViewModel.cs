using Liquidity2.UI.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Liquidity2.UI.Windows.TOS
{
    public class TOSViewModel : INotifyPropertyChanged
    {
        private readonly IWindowCommonBehavior _windowCommonBehavior;

        public ICommand WindowDragMoveCmd => _windowCommonBehavior.WindowDragMoveCmd;
        public ICommand WindowMinimizeCmd => _windowCommonBehavior.WindowMinimizeCmd;
        public ICommand WindowMaximizeCmd => _windowCommonBehavior.WindowMaximizeCmd;
        public ICommand WindowCloseCmd => _windowCommonBehavior.WindowCloseCmd;
        public ICommand WindowSearchedCmd => _windowCommonBehavior.WindowSearchCmd;
        public ICommand WindowGroupedCmd => _windowCommonBehavior.WindowGroupCmd;

        public ICommand PrecisionChangeCmd { get; } = new CustomRoutedCommand(nameof(PrecisionChangeCmd), typeof(TOSWindow));
        public ICommand CreateBuyOrderCmd { get; } = new CustomRoutedCommand(nameof(CreateBuyOrderCmd), typeof(TOSWindow));
        public ICommand CreateSellOrderCmd { get; } = new CustomRoutedCommand(nameof(CreateSellOrderCmd), typeof(TOSWindow));
        public ICommand StarButtonClickCmd { get; set; } = new CustomRoutedCommand(nameof(StarButtonClickCmd), typeof(TOSWindow));

        private string symbol;
        private string group;
        private ObservableCollection<TOSData> listData;
        private ObservableCollection<L2Data> sellListData;
        private ObservableCollection<L2Data> buyListData;
        private ObservableCollection<string> precision;

        //显示的精度
        private string selectPrecision;
        public string SelectPrecision
        {
            get => selectPrecision;
            set
            {
                selectPrecision = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectPrecision)));
            }
        }

        //当前精度
        public int NowPrecision { get; set; }

        //最大精度
        public int MaxPrecision { get; set; }

        private Brush windowBorderBrush;
        public Brush WindowBorderBrush
        {
            get => windowBorderBrush;
            set
            {
                windowBorderBrush = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WindowBorderBrush)));
            }
        }

        //数量头部标识
        private string amountHeader;
        public string AmountHeader
        {
            get => amountHeader;
            set
            {
                amountHeader = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AmountHeader)));
            }
        }

        //价格头部标识
        private string priceHeader;
        public string PriceHeader
        {
            get => priceHeader;
            set
            {
                priceHeader = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PriceHeader)));
            }
        }

        private string tosTitle;

        public string TosTitle
        {
            get { return tosTitle; }
            set
            {
                tosTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TosTitle)));
            }
        }

        //买单量 
        private decimal buyCount;
        public decimal BuyCount
        {
            get => buyCount;
            set
            {
                buyCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyCount)));
            }
        }

        //买单最高价格
        private decimal buyTopPrice;
        public decimal BuyTopPrice
        {
            get => buyTopPrice;
            set
            {
                buyTopPrice = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyTopPrice)));
            }
        }

        //买单百分比
        private string _buyPercentage;
        public string BuyPercentage
        {
            get => _buyPercentage;
            set
            {
                _buyPercentage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyPercentage)));
            }
        }

        //买单柱形条
        private GridLength _buyHistogram;
        public GridLength BuyHistogram
        {
            get => _buyHistogram;
            set
            {
                _buyHistogram = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyHistogram)));
            }
        }

        //卖单量
        private decimal sellCount;
        public decimal SellCount
        {
            get => sellCount;
            set
            {
                sellCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellCount)));
            }
        }

        //卖单最高价格
        private decimal sellTopPrice;
        public decimal SellTopPrice
        {
            get => sellTopPrice;
            set
            {
                sellTopPrice = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellTopPrice)));
            }
        }

        //卖单百分比
        private string _sellPercentage;
        public string SellPercentage
        {
            get => _sellPercentage;
            set
            {
                _sellPercentage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellPercentage)));
            }
        }

        //卖单柱形条
        private GridLength _sellHistogram;
        public GridLength SellHistogram
        {
            get => _sellHistogram;
            set
            {
                _sellHistogram = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellHistogram)));
            }
        }

        //判断是否为自选币对
        private bool isSelfSelect;
        public bool IsSelfSelect
        {
            get { return isSelfSelect; }
            set
            {
                isSelfSelect = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelfSelect)));
            }
        }


        public TOSViewModel(IWindowCommonBehavior windowCommonBehavior)
        {
            windowCommonBehavior.BorderColorChanged += BorderColorChanged;
            _windowCommonBehavior = windowCommonBehavior;
            ListData = new ObservableCollection<TOSData>();
            BuyListData = new ObservableCollection<L2Data>();
            SellListData = new ObservableCollection<L2Data>();
            Precision = new ObservableCollection<string>();
        }

        private void BorderColorChanged(object sender, PropertyChangedEventArgs e)
        {
            WindowBorderBrush = new SolidColorBrush(_windowCommonBehavior.BorderColor);
        }



        public string Symbol
        {
            get => symbol;
            set
            {
                symbol = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Symbol)));
            }
        }
        public string Group
        {
            get => group;
            set
            {
                group = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Group)));
            }
        }

        public ObservableCollection<TOSData> ListData
        {
            get => listData;
            set
            {
                listData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListData)));
            }
        }

        public ObservableCollection<string> Precision
        {
            get => precision;
            set
            {
                precision = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Precision)));
            }
        }

        public ObservableCollection<L2Data> BuyListData
        {
            get => buyListData;
            set
            {
                buyListData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyListData)));
            }
        }

        public ObservableCollection<L2Data> SellListData
        {
            get => sellListData;
            set
            {
                sellListData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellListData)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
