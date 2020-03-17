using System;

namespace Liquidity2.Extensions.EventBus.EventObserver
{
    public interface IEventObserverBuilder
    {
        void AddObserverFromExisting<TImplementation>() where TImplementation : class, IEventObserver;

        void AddObserverFromExisting<TService, TImplementation>() where TImplementation : class, IEventObserver, TService;

        void AddSingletonEventObserver<TImplementation>() where TImplementation : class, IEventObserver;

        void AddSingletonEventObserver<TImplementation>(Func<IServiceProvider, TImplementation> factory) where TImplementation : class, IEventObserver;

        void AddSingletonEventObserver<TService, TImplementation>() where TImplementation : class, IEventObserver, TService;

        void AddSingletonEventObserver<TService, TImplementation>(Func<IServiceProvider, TImplementation> factory) where TImplementation : class, IEventObserver, TService;
    }
}
