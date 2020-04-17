using Liquidity2.Data.Client.Abstractions.Market;
using System.Threading.Tasks;

namespace Liquidity2.Service.SubscribeManager
{
    public interface ISubscribeManager<T> where T : class
    {
        void AddSubscribe(string symbol, MarketSubscribeDataType type);

        void RemoveSubscribe(string symbol, MarketSubscribeDataType type);

        T Subject { get; set; }
    }
}
