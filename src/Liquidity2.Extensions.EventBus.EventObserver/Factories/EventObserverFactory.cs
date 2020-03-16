using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Liquidity2.Extensions.EventBus.EventObserver
{
    public class EventObserverFactory : IEventObserverFactory
    {
        private readonly IServiceProvider _provider;

        public EventObserverFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<IEventObserver> Create()
        {
            return _provider.GetServices<IEventObserver>();
        }
    }
}
