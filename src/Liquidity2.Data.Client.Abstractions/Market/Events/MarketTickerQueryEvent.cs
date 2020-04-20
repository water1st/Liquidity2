using Liquidity2.Data.Client.Abstractions.DTO;
using Liquidity2.Extensions.EventBus;
using System.Collections.Generic;

namespace Liquidity2.Data.Client.Abstractions.Market.Events
{
    public class MarketTickerQueryEvent : Event
    {
        public MarketTickerQueryEvent(IEnumerable<Ticker> tickers)
        {
            Tickers = tickers;
        }

        /// <summary>
        /// Ticker集合
        /// </summary>
        public IEnumerable<Ticker> Tickers { get; set; }
    }
}
