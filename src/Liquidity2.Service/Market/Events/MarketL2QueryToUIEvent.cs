using Liquidity2.Extensions.EventBus;
using Liquidity2.Service.Market.DTO;
using System.Collections.Generic;

namespace Liquidity2.Service.Market.Events
{
    public class MarketL2QueryToUIEvent:Event
    {
        public MarketL2QueryToUIEvent(string symbol, IEnumerable<L2Item> buyTradeItems, IEnumerable<L2Item> sellTradeItems, int precision)
        {
            Symbol = symbol;
            BuyTradeItems = buyTradeItems;
            SellTradeItems = sellTradeItems;
            Precision = precision;
        }

        /// <summary>
        /// 代码
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// 精度
        /// </summary>
        public int Precision { get; }

        /// <summary>
        /// L2买盘集合
        /// </summary>
        public IEnumerable<L2Item> BuyTradeItems { get; }

        /// <summary>
        /// L2卖盘集合
        /// </summary>
        public IEnumerable<L2Item> SellTradeItems { get; }

    }
}
