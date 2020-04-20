using Liquidity2.Data.Client.Abstractions.Market;
using Liquidity2.Data.Client.Abstractions.Market.SubscribeModel;
using Liquidity2.Utilities;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Liquidity2.Service.SubscribeManager
{
    public class SubscribeManager<T> : ISubscribeManager<T> where T : SubscribeModel
    {
        private readonly ConcurrentDictionary<string, SubscribeModel> subscribes;

        private readonly ISubject<T> _subject;

        public SubscribeManager(ISubject<T> subject)
        {
            _subject = subject;
            subscribes = new ConcurrentDictionary<string, SubscribeModel>();
        }

        public async Task<IDisposable> AddSubscribe(T subscribeModel)
        {
            var subscribe = subscribes.GetOrAdd($"[{subscribeModel.Type}][{subscribeModel.Symbol}]", name => subscribeModel);
            subscribe.AddObserver();

            if (subscribe.ObserverCount == 1)
            {
                await Subscribe(subscribeModel);
            }
            var disposabler = new Disposabler(async () => {
                await RemoveSubscribe(subscribeModel);
            });
            return disposabler;
        }

        public async Task RemoveSubscribe(T subscribeModel)
        {
            var subscribe = subscribes.GetOrAdd($"[{subscribeModel.Type}][{subscribeModel.Symbol}]", name => subscribeModel);

            subscribe.ReduceObserver();
            if (subscribe.ObserverCount == 0)
            {
                await Unsubscribe(subscribe);
            }
        }

        public async Task<IDisposable> Subscribe(T SubscribeModel)
        {
            var disposabler = await _subject.Subscribe(SubscribeModel);
            return disposabler;
        }

        public async Task Unsubscribe(SubscribeModel unSubscribeModel)
        {
            await _subject.Unsubscribe(unSubscribeModel);
        }
    }
}
