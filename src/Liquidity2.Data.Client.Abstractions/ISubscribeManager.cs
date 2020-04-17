using Liquidity2.Data.Client.Abstractions.Market;
using System.Threading.Tasks;

namespace Liquidity2.Data.Client.Abstractions
{
    public interface ISubscribeManager
    {
        void AddSubscribe(string symbol, MarketSubscribeDataType type);

        void RemoveSubscribe(string symbol, MarketSubscribeDataType type);
    }
}
