using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Services.TOS.DTO;
using System.Collections.Generic;

namespace Liquidity2.UI.Services.TOS.Events
{
    public class L2DataQueryEvent:Event
    {
        public L2DataQueryEvent(string symbol, IEnumerable<L2Item> buyL2Items, IEnumerable<L2Item> sellL2Items, int precision)
        {
            Symbol = symbol;
            BuyTradeItems = buyL2Items;
            SellTradeItems = sellL2Items;
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
