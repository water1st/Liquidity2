using Liquidity2.Data.Client.Abstractions.DTO;
using Liquidity2.Extensions.EventBus;
using System.Collections.Generic;

namespace Liquidity2.Data.Client.Abstractions.Market.Events
{
    public class MarketKLineMinQueryEvent : Event
    {
        public MarketKLineMinQueryEvent(string symbol, IEnumerable<KLineItem> kLineItems)
        {
            Symbol = symbol;
            KLineItems = kLineItems;
        }

        public string Symbol { get; set; }
        public IEnumerable<KLineItem> KLineItems { get; set; }

    }
}
