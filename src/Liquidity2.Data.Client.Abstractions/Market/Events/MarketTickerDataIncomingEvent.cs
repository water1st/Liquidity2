using Liquidity2.Data.Client.Abstractions.DTO;
using Liquidity2.Extensions.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liquidity2.Data.Client.Abstractions.Market.Events
{
    public class MarketTickerDataIncomingEvent:Event
    {
        public MarketTickerDataIncomingEvent(IEnumerable<Ticker> tickers)
        {
            Tickers = tickers;
        }

        /// <summary>
        /// Ticker集合
        /// </summary>
        public IEnumerable<Ticker> Tickers { get; set; }
    }
}
