using Liquidity2.Data.Client.Abstractions.Market.SubscribeModel;
using System;

namespace Liquidity2.Service.SubscribeManager
{
    public class SubscribeManagerFactory : ISubscribeManagerFactory
    {
        private readonly IServiceProvider _provider;

        public SubscribeManagerFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public ISubscribeManager<T> Create<T>() where T : SubscribeModel
        {
            return _provider.GetService(typeof(ISubscribeManager<T>)) as ISubscribeManager<T>;
        }
    }
}
