using System;
using System.Threading.Tasks;

namespace Liquidity2.UI.Services.TOS
{
    public interface IMarketObsever: IDisposable
    {
        ExactSymbol ExactSymbol { get; }
    }
}
