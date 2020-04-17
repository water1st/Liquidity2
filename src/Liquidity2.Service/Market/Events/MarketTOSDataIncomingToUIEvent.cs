using Liquidity2.Extensions.EventBus;
using Liquidity2.Service.Market.DTO;
using System.Collections.Generic;

namespace Liquidity2.Service.Market.Events
{
    public class MarketTOSDataIncomingToUIEvent:Event
    {
        public MarketTOSDataIncomingToUIEvent(string symbol, IEnumerable<TOSItem> tosItems)
        {
            Symbol = symbol;
            TOSItems = tosItems;
        }

        /// <summary>
        /// 代码
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// TOS集合
        /// </summary>
        public IEnumerable<TOSItem> TOSItems { get; }
    }
}
