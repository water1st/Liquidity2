using Liquidity2.Service.Market.DTO;
using System;

namespace Liquidity2.Service.Market
{
    public class MarketObserver : IMarketObsever
    {
        private readonly MarketSubscribeDataType type;
        private readonly Action func;
        private bool unsubscribe;

        public MarketObserver(string symbol, MarketSubscribeDataType type, Action func, int precision = 0)
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

            func.Invoke();
        }
    }
}
