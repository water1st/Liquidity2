using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Services.DTO;
using Liquidity2.UI.Services.TOS.DTO;
using System.Collections.Generic;

namespace Liquidity2.UI.Services.TOS.Events
{
    public class L2DataIncomingEvent:Event
    {
        public L2DataIncomingEvent(string symbol, IEnumerable<L2Item> l2Items, TradeDirection side, int precision)
        {
            Symbol = symbol;
            TradeItems = l2Items;
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
        public IEnumerable<L2Item> TradeItems { get; }

        /// <summary>
        /// 买卖方向
        /// </summary>
        public TradeDirection Side { get; }
    }
}
