using Liquidity2.Service.DTO;
using System;

namespace Liquidity2.Service.Market.DTO
{
    public class L2Item
    {
        public L2Item(decimal amount, decimal price, int count, ExchangeType exchange)
        {
            Amount = Convert.ToDecimal(amount);
            Count = count;
            Price = Convert.ToDecimal(price);
            Exchange = exchange;
        }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// 订单量
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// 交易所
        /// </summary>
        public ExchangeType Exchange { get; }
    }
}
