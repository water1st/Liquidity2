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
                this.type = type;
                this.func = func;
                unsubscribe = false;
                ExactSymbol = new ExactSymbol { Precision = precision, Symbol = symbol };
            }

            public ExactSymbol ExactSymbol { get; set; }

            public void Dispose()
            {
                if (unsubscribe)
                    return;

                func(ExactSymbol.Symbol, type, ExactSymbol.Precision);
            }
        }
    }
}
