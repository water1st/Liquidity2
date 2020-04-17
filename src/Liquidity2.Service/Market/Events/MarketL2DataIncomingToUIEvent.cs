using Liquidity2.Extensions.EventBus;
using Liquidity2.Service.DTO;
using Liquidity2.Service.Market.DTO;
using System.Collections.Generic;

namespace Liquidity2.Service.Market.Events
{
    public class MarketL2DataIncomingToUIEvent:Event
    {
        public MarketL2DataIncomingToUIEvent(string symbol, IEnumerable<L2Item> tradeItems, TradeDirection side, int precision)
        {
            Symbol = symbol;
            T2Items = tradeItems;
            Side = side;
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
        /// L2集合
        /// </summary>
        public IEnumerable<L2Item> T2Items { get; }

        /// <summary>
        /// 买卖方向
        /// </summary>
        public TradeDirection Side { get; }

    }
}
