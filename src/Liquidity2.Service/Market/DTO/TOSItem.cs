using Liquidity2.Service.DTO;
using System;

namespace Liquidity2.Service.Market.DTO
{
    public class TOSItem
    {
        public TOSItem(decimal amount, decimal price, TradeDirection side, DateTimeOffset time, ExchangeType exchangeType)
        {
            Amount = amount;
            Price = price;
            Side = side;
            Time = time;
            ExchangeType = exchangeType;
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
        /// 买卖方向
        /// </summary>
        public TradeDirection Side { get; }

        /// <summary>
        /// 成交时间
        /// </summary>
        public DateTimeOffset Time { get; }

        /// <summary>
        /// 交易所
        /// </summary>
        public ExchangeType ExchangeType { get; }
    }
}
