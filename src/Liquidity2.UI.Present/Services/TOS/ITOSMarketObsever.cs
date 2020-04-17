using System.Threading.Tasks;

namespace Liquidity2.UI.Services.TOS
{
    public interface ITOSMarketObsever
    {
        string Symbol { get; }

        Task Unsubscribe();
    }
}
