using Liquidity2.Data.Client.Abstractions.Market.SubscribeModel;
using System;
using System.Threading.Tasks;

namespace Liquidity2.Data.Client.Abstractions.Market
{
    public interface ISubject<T> where T: SubscribeModel.SubscribeModel
    {
        Task<IDisposable> Subscribe(T subscribeModel);

        Task Unsubscribe(SubscribeModel.SubscribeModel unSubscribeModel);
    }
}
