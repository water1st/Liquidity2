using System;

namespace Liquidity2.Extensions.EventBus.EventObserver
{
    public interface IEventBusRegistrator
    {
        IDisposable Register<TEvent>(IEventHandler<TEvent> handler) where TEvent : Event;
    }
}
