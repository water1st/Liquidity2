using System;
using System.Collections.Generic;
using System.Text;

namespace Liquidity2.Service.Market
{
    public interface IMarketObsever : IDisposable
    {
        ExactSymbol ExactSymbol { get; }
    }
}
