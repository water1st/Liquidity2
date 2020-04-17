using System.Threading.Tasks;

namespace Liquidity2.UI.Services.TOS
{
    public interface IL2MarketObsever
    {
        string Symbol { get; }
        Task Unsubscribe();
    }
}
