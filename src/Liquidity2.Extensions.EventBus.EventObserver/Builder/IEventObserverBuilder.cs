using System;

namespace Liquidity2.Extensions.EventBus.EventObserver
{
    public interface IEventObserverBuilder
    {
        void AddObserverFromExisting<TImplementation>() where TImplementation : class, IEventObserver;

        void AddObserverFromExisting<TService, TImplementation>() where TImplementation : class, IEventObserver, TService;

        void AddEventObserverWithSingleton<TImplementation>() where TImplementation : class, IEventObserver;

        void AddEventObserverWithSingleton<TImplementation>(Func<IServiceProvider, TImplementation> factory) where TImplementation : class, IEventObserver;

        void AddEventObserverWithSingleton<TService, TImplementation>() where TImplementation : class, IEventObserver, TService;

        void AddEventObserverWithSingleton<TService, TImplementation>(Func<IServiceProvider, TImplementation> factory) where TImplementation : class, IEventObserver, TService;
    }
}
