using Liquidity2.Data.Client.Abstractions.DTO;
using Liquidity2.Extensions.EventBus;

namespace Liquidity2.Data.Client.Abstractions.Market.Events
{
    public class MarketCandleDataIncomingEvent:Event
    {
        public MarketCandleDataIncomingEvent(KLineItem candleItem, string symbol)
        {
            CandleItem = candleItem;
            Symbol = symbol;
        }

        public KLineItem CandleItem { get; set; }

        public string Symbol { get; set; }
    }
}
