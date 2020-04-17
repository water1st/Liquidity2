using Liquidity2.UI.Services.DTO;
using System.ComponentModel;

namespace Liquidity2.UI.Windows.TOS
{
    public class L2Data : INotifyPropertyChanged
    {
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 订单量
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// 交易所
        /// </summary>
        public ExchangeType Exchange { get; }

        /// <summary>
        /// 价格分组
        /// </summary>
        private int _group;
        public int Group
        {
            get => _group;
            set
            {
                _group = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Group)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public L2Data(decimal price, decimal amount, int count, ExchangeType exchange)
        {
            Price = price;
            Amount = amount;
            Count = count;
            Exchange = exchange;
        }
    }
}
