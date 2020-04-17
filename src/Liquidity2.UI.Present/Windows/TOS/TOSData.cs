using Liquidity2.UI.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liquidity2.UI.Windows.TOS
{
    public class TOSData
    {
        /// <summary>
        /// 交易对
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// 买卖方向
        /// </summary>
        public TradeDirection Direction { get; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// 交易所
        /// </summary>
        public ExchangeType Exchange { get; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; }

        public TOSData(decimal price, decimal amount, string time, TradeDirection side, ExchangeType exchange)
        {
            Price = price;
            Amount = amount;
            Time = time;
            Direction = side;
            Exchange = exchange;
        }
    }
}
