using Liquidity2.Data.Client.Abstractions.Market.SubscribeModel;

namespace Liquidity2.Service.SubscribeManager
{
    public interface ISubscribeManagerFactory
    {
        ISubscribeManager<T> Create<T>() where T : SubscribeModel;
    }
}
