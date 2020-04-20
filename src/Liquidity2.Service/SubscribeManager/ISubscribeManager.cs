using Liquidity2.Data.Client.Abstractions.Market;
using Liquidity2.Data.Client.Abstractions.Market.SubscribeModel;
using System;
using System.Threading.Tasks;

namespace Liquidity2.Service.SubscribeManager
{
    public interface ISubscribeManager<T>:ISubject<T> where T : SubscribeModel
    {
        Task<IDisposable> AddSubscribe(T subscribeModel);

        Task RemoveSubscribe(T subscribeModel);
    }
}
