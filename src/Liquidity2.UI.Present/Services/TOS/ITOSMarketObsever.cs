using System.Threading.Tasks;

namespace Liquidity2.UI.Services.TOS
{
    public interface IMarketObsever
    {
        string Symbol { get; }

        Task Unsubscribe();
    }
}
