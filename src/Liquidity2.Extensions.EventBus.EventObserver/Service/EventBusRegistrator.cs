using System;

namespace Liquidity2.Extensions.EventBus.EventObserver
{
    internal class EventBusRegistrator : IEventBusRegistrator
    {
        private readonly IEventBus eventBus;

        public EventBusRegistrator(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public IDisposable Register<TEvent>(IEventHandler<TEvent> handler) where TEvent : Event
        {
            return eventBus.Subscribe(handler);
        }
    }
}
