using Liquidity2.Extensions.EventBus.EventObserver;
using Liquidity2.Service.Market.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Liquidity2.UI.Services.TOS
{
    public partial class TOSService
    {
        private class MarketObserver : IMarketObsever
        {
            private readonly MarketSubscribeDataType type;
            private readonly Func<string, MarketSubscribeDataType, int, Task> func;
            private bool unsubscribe;

            public MarketObserver(string symbol, MarketSubscribeDataType type, Func<string, MarketSubscribeDataType, int, Task> func, int precision = 0)
            {
                Symbol = symbol;
                this.type = type;
                this.func = func;
                unsubscribe = false;
                Precision = precision;
            }

            public string Symbol { get; }
            public int Precision { get; }

            public async Task Unsubscribe()
            {
                if (unsubscribe)
                    return;

                await func(Symbol, type, Precision);
            }
        }
    }
}
