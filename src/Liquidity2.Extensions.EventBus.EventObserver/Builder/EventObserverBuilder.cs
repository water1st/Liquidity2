using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.EventBus.EventObserver
{
    public class EventObserverBuilder : IEventObserverBuilder
    {
        private readonly IServiceCollection _services;
        public EventObserverBuilder(IServiceCollection services) => _services = services;

        public void AddObserverFromExisting<TImplementation>() where TImplementation : class, IEventObserver => _services.AddObserverFromExisting<TImplementation>();

        public void AddObserverFromExisting<TService, TImplementation>() where TImplementation : class, IEventObserver, TService => _services.AddObserverFromExisting<TService, TImplementation>();

        public void AddSingletonEventObserver<TImplementation>() where TImplementation : class, IEventObserver => _services.AddSingletonEventObserver<TImplementation>();

        public void AddSingletonEventObserver<TImplementation>(Func<IServiceProvider, TImplementation> factory) where TImplementation : class, IEventObserver => _services.AddSingletonEventObserver(factory);

        public void AddSingletonEventObserver<TService, TImplementation>() where TImplementation : class, IEventObserver, TService => _services.AddSingletonEventObserver<TService, TImplementation>();

        public void AddSingletonEventObserver<TService, TImplementation>(Func<IServiceProvider, TImplementation> factory) where TImplementation : class, IEventObserver, TService => _services.AddSingletonEventObserver<TService, TImplementation>(factory);
    }
}
