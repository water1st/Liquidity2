using Liquidity2.Data.Client.Abstractions.Market;
using System;

namespace Liquidity2.Data.Client.Api.Subjects
{
    public partial class MarketSubject
    {
        internal class SubscribeModel
        {
            public MarketSubscribeDataType SubscribeType { get; }
            public string Symbol { get; }
            public int ObserverCount { get; set; }
            public string Id { get; set; }
            public int Precision { get; set; }

            public SubscribeModel(MarketSubscribeDataType type, string symbol)
            {
                SubscribeType = type;
                Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            }

            public void AddObserver(int quantity = 1)
            {
                ObserverCount += quantity;
            }

            public void ReduceObserver(int quantity = 1)
            {
                ObserverCount -= quantity;
            }
        }
    }
}
