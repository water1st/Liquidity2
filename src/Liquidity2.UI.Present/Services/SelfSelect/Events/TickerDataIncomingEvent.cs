using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Services.SelfSelect.DTO;
using System.Collections.Generic;

namespace Liquidity2.UI.Services.SelfSelect.Events
{
    public class TickerDataIncomingEvent:Event
    {

        public TickerDataIncomingEvent(IEnumerable<Ticker> tickers)
        {
            Tickers = tickers;
        }

        /// <summary>
        /// Ticker集合
        /// </summary>
        public IEnumerable<Ticker> Tickers { get; set; }
    }
}
