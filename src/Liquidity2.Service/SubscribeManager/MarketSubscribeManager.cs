using Liquidity2.Data.Client.Abstractions.Market;
using System;
using System.Threading.Tasks;

namespace Liquidity2.Service.SubscribeManager
{
    public class MarketSubscribeManager : ISubscribeManager<IMarketSubject>
    {
        public IMarketSubject Subject { get; set; }

        public MarketSubscribeManager(IMarketSubject subject)
        {
            Subject = subject;
        }

        public void AddSubscribe(string symbol, MarketSubscribeDataType type)
        {
            Subject.AddSubscribe(symbol, type);
        }

        public void RemoveSubscribe(string symbol, MarketSubscribeDataType type)
        {
            Subject.RemoveSubscribe(symbol, type);
        }
    }
}
