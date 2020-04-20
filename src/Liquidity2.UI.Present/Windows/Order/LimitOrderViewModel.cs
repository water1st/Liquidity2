using Liquidity2.UI.Services.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace Liquidity2.UI.Windows
{
    public class LimitOrderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _toCurrency = "";
        /// <summary>
        /// 买入币种
        /// </summary>
        public string ToCurrency
        {
            get => _toCurrency;
            set
            {
                _toCurrency = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToCurrency)));
            }
        }

        private string _fromCurrency = "";
        /// <summary>
        /// 卖出币种
        /// </summary>
        public string FromCurrency
        {
            get => _fromCurrency;
            set
            {
                _fromCurrency = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FromCurrency)));
            }
        }

        /// <summary>
        /// 交易所列表
        /// </summary>
        public List<ExchangeType> ExchangeList { get; } = new List<ExchangeType> { ExchangeType.Bitfinex, ExchangeType.Huobi, ExchangeType.Okex };

        private decimal _buyingAvailable = 0;
        /// <summary>
        /// 买入可用
        /// </summary>
        public decimal BuyingAvailable
        {
            get => _buyingAvailable;
            set
            {
                _buyingAvailable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingAvailable)));
                BuyingSliderDotBrushChange();
            }
        }

        private string _buyingPriceString;
        /// <summary>
        /// 买入价绑定字符串
        /// </summary>
        public string BuyingPriceString
        {
            get => _buyingPriceString;
            set
            {
                _buyingPriceString = value;
                if (!string.IsNullOrEmpty(_buyingPriceString) && _buyingVolumeString != ".")
                {
                    _buyingPrice = Convert.ToDecimal(_buyingPriceString);
                }
                else { _buyingPrice = 0; }
                _buyingVolume = _buyingAmount * _buyingPrice;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingVolume)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingPrice)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingTransactionAmount)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingPriceString)));
                BuyingSliderDotBrushChange();
            }
        }

        private void BuyingSliderDotBrushChange()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingSliderDotBrush2)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingSliderDotBrush3)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingSliderDotBrush4)));
        }

        private decimal _buyingPrice;
        /// <summary>
        /// 买入价
        /// </summary>
        public decimal BuyingPrice
        {
            get => _buyingPrice;
            set
            {
                _buyingPrice = value;
                BuyingVolume = _buyingAmount * _buyingPrice;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingPrice)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingTransactionAmount)));
            }
        }

        private string _buyingVolumeString;
        /// <summary>
        /// 买入量绑定字符串
        /// </summary>
        public string BuyingVolumeString
        {
            get => _buyingVolumeString;
            set
            {
                _buyingVolumeString = value;
                if (!string.IsNullOrEmpty(_buyingVolumeString) && _buyingVolumeString != ".")
                {
                    BuyingAmount = Convert.ToDecimal(_buyingVolumeString);
                }
                else { _buyingAmount = 0; _buyingVolume = 0; }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingAmount)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingVolume)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingTransactionAmount)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingVolumeString)));
                BuyingSliderDotBrushChange();
            }
        }

        private decimal _buyingVolume;
        /// <summary>
        /// 买入量
        /// </summary>
        public decimal BuyingVolume
        {
            get => _buyingVolume;
            set
            {
                _buyingVolume = value;
                if (_buyingPrice != 0)
                {
                    //Okex的精确度为8位
                    if (BuyingExchange == ExchangeType.Okex)
                    {
                        _buyingAmount = Math.Round(_buyingVolume / _buyingPrice, 8, MidpointRounding.ToNegativeInfinity);
                    }
                    //Huobi的精度位4位
                    else if (BuyingExchange == ExchangeType.Huobi)
                    {
                        _buyingAmount = Math.Round(_buyingVolume / _buyingPrice, 4, MidpointRounding.ToNegativeInfinity);
                    }
                }
                _buyingVolumeString = _buyingAmount.ToString();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingVolumeString)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingAmount)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingVolume)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingTransactionAmount)));
                BuyingSliderDotBrushChange();
            }
        }

        private decimal _buyingAmount;
        /// <summary>
        /// 买入币量
        /// </summary>
        public decimal BuyingAmount
        {
            get => _buyingAmount;
            set
            {
                if (_buyingAmount != value)
                {
                    _buyingAmount = value;
                    BuyingVolume = _buyingAmount * _buyingPrice;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingVolumeString)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingAmount)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingTransactionAmount)));
                }
            }
        }

        private ExchangeType _buyingExchange;
        /// <summary>
        /// 买入交易所
        /// </summary>
        public ExchangeType BuyingExchange
        {
            get => _buyingExchange;
            set
            {
                _buyingExchange = value;
                BuyingVolume = _buyingVolume;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyingExchange)));
            }
        }

        /// <summary>
        /// 买入交易额
        /// </summary>
        public decimal BuyingTransactionAmount => Math.Round(_buyingPrice * _buyingAmount, 8);

        private decimal _sellingAvailable = 0;
        /// <summary>
        /// 卖出可用
        /// </summary>
        public decimal SellingAvailable
        {
            get => _sellingAvailable;
            set
            {
                _sellingAvailable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingAvailable)));

                SellingSliderDotBrushChange();
            }
        }

        private void SellingSliderDotBrushChange()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingSliderDotBrush2)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingSliderDotBrush3)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingSliderDotBrush4)));
        }

        private string _sellingPriceString;
        /// <summary>
        /// 卖出价绑定字符串
        /// </summary>
        public string SellingPriceString
        {
            get => _sellingPriceString;
            set
            {
                _sellingPriceString = value;
                if (!string.IsNullOrEmpty(_sellingPriceString) && _buyingVolumeString != ".")
                {
                    SellingPrice = Convert.ToDecimal(_sellingPriceString);
                }
                else { SellingPrice = 0; }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingPriceString)));
            }
        }

        private decimal _sellingPrice;
        /// <summary>
        /// 卖出价
        /// </summary>
        public decimal SellingPrice
        {
            get => _sellingPrice;
            set
            {
                _sellingPrice = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingPrice)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingTransactionAmount)));
            }
        }

        private string _sellingVolumeString;
        /// <summary>
        /// 卖出量绑定字符串
        /// </summary>
        public string SellingVolumeString
        {
            get => _sellingVolumeString;
            set
            {
                _sellingVolumeString = value;
                if (!string.IsNullOrEmpty(_sellingVolumeString) && _buyingVolumeString != ".")
                {
                    //保留8位小数
                    _sellingVolume = Convert.ToDecimal(_sellingVolumeString);
                    if (_sellingVolumeString.IndexOf(".") != -1 && (_sellingVolumeString.Length - _sellingVolumeString.IndexOf(".") - 1 > 8)&&SellingExchange == ExchangeType.Okex)
                    {
                        _sellingVolumeString = _sellingVolumeString.Substring(0, _sellingVolumeString.Length-1);
                        _sellingVolume = decimal.Round(_sellingVolume, 8);
                    }
                    else if (_sellingVolumeString.IndexOf(".") != -1 && (_sellingVolumeString.Length - _sellingVolumeString.IndexOf(".") - 1 > 4) && SellingExchange == ExchangeType.Huobi)
                    {
                        _sellingVolumeString = _sellingVolumeString.Substring(0, _sellingVolumeString.Length - 1);
                        _sellingVolume = decimal.Round(_sellingVolume, 4);
                    }
                }
                else { _sellingVolume = 0; }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingVolume)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingTransactionAmount)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingVolumeString)));
                SellingSliderDotBrushChange();
            }
        }

        private decimal _sellingVolume;
        /// <summary>
        /// 卖出量
        /// </summary>
        public decimal SellingVolume
        {
            get => _sellingVolume;
            set
            {
                _sellingVolume = Math.Round(value, 8, MidpointRounding.ToNegativeInfinity);
                _sellingVolumeString = _sellingVolume.ToString();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingVolumeString)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingVolume)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingTransactionAmount)));
                SellingSliderDotBrushChange();
            }
        }

        private ExchangeType _sellingExchange;
        /// <summary>
        /// 卖出交易所
        /// </summary>
        public ExchangeType SellingExchange
        {
            get => _sellingExchange;
            set
            {
                _sellingExchange = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellingExchange)));
            }
        }

        /// <summary>
        /// 卖出交易额
        /// </summary>
        public decimal SellingTransactionAmount => Math.Round(_sellingPrice * _sellingVolume, 8);

        private string buyErrorMessage;

        public string BuyErrorMessage
        {
            get { return buyErrorMessage; }
            set { buyErrorMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BuyErrorMessage)));
            }
        }

        private string sellErrorMessage;

        public string SellErrorMessage
        {
            get { return sellErrorMessage; }
            set { sellErrorMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SellErrorMessage)));
            }
        }


        private readonly SolidColorBrush _defaultSliderDotBrush = new SolidColorBrush(Color.FromRgb(0x39, 0x45, 0x6D));

        private readonly SolidColorBrush _buyingSliderDotBrush = new SolidColorBrush(Color.FromRgb(0x15, 0x64, 0xF7));

        private readonly SolidColorBrush _sellingSliderDotBrush = new SolidColorBrush(Color.FromRgb(0xEE, 0x65, 0x60));

        public ICommand BuyingSliderDot1ClickCmd { get; } = new CustomRoutedCommand(nameof(BuyingSliderDot1ClickCmd), typeof(OrderWindow));

        public ICommand BuyingSliderDot2ClickCmd { get; } = new CustomRoutedCommand(nameof(BuyingSliderDot2ClickCmd), typeof(OrderWindow));
        public SolidColorBrush BuyingSliderDotBrush2 => BuyingAvailable == 0 ? _buyingSliderDotBrush : BuyingVolume / BuyingAvailable >= 0.25M ? _buyingSliderDotBrush : _defaultSliderDotBrush;

        public ICommand BuyingSliderDot3ClickCmd { get; } = new CustomRoutedCommand(nameof(BuyingSliderDot3ClickCmd), typeof(OrderWindow));
        public SolidColorBrush BuyingSliderDotBrush3 => BuyingAvailable == 0 ? _buyingSliderDotBrush : BuyingVolume / BuyingAvailable >= 0.5M ? _buyingSliderDotBrush : _defaultSliderDotBrush;

        public ICommand BuyingSliderDot4ClickCmd { get; } = new CustomRoutedCommand(nameof(BuyingSliderDot4ClickCmd), typeof(OrderWindow));
        public SolidColorBrush BuyingSliderDotBrush4 => BuyingAvailable == 0 ? _buyingSliderDotBrush : BuyingVolume / BuyingAvailable >= 0.75M ? _buyingSliderDotBrush : _defaultSliderDotBrush;

        public ICommand BuyingSliderDot5ClickCmd { get; } = new CustomRoutedCommand(nameof(BuyingSliderDot5ClickCmd), typeof(OrderWindow));

        public ICommand SellingSliderDot1ClickCmd { get; } = new CustomRoutedCommand(nameof(SellingSliderDot1ClickCmd), typeof(OrderWindow));

        public ICommand SellingSliderDot2ClickCmd { get; } = new CustomRoutedCommand(nameof(SellingSliderDot2ClickCmd), typeof(OrderWindow));
        public SolidColorBrush SellingSliderDotBrush2 => SellingAvailable == 0 ? _sellingSliderDotBrush : SellingVolume / SellingAvailable >= 0.25M ? _sellingSliderDotBrush : _defaultSliderDotBrush;

        public ICommand SellingSliderDot3ClickCmd { get; } = new CustomRoutedCommand(nameof(SellingSliderDot3ClickCmd), typeof(OrderWindow));
        public SolidColorBrush SellingSliderDotBrush3 => SellingAvailable == 0 ? _sellingSliderDotBrush : SellingVolume / SellingAvailable >= 0.5M ? _sellingSliderDotBrush : _defaultSliderDotBrush;

        public ICommand SellingSliderDot4ClickCmd { get; } = new CustomRoutedCommand(nameof(SellingSliderDot4ClickCmd), typeof(OrderWindow));
        public SolidColorBrush SellingSliderDotBrush4 => SellingAvailable == 0 ? _sellingSliderDotBrush : SellingVolume / SellingAvailable >= 0.75M ? _sellingSliderDotBrush : _defaultSliderDotBrush;

        public ICommand SellingSliderDot5ClickCmd { get; } = new CustomRoutedCommand(nameof(SellingSliderDot5ClickCmd), typeof(OrderWindow));

        public ICommand LimitBuyButtonClickCmd { get; } = new CustomRoutedCommand(nameof(LimitBuyButtonClickCmd), typeof(OrderWindow));

        public ICommand LimitSellButtonClickCmd { get; } = new CustomRoutedCommand(nameof(LimitSellButtonClickCmd), typeof(OrderWindow));

    }
}
