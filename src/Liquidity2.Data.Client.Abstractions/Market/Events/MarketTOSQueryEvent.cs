using Liquidity2.Data.Client.Abstractions.DTO;
using Liquidity2.Extensions.EventBus;
using System.Collections.Generic;

namespace Liquidity2.Data.Client.Abstractions.Market.Events
{
    public class MarketTOSQueryEvent : Event
    {
        public MarketTOSQueryEvent(string symbol, IEnumerable<TOSItem> tosItems)
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
