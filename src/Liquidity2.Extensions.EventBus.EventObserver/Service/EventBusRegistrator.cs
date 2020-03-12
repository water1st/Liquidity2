using System;

namespace Liquidity2.Extensions.EventBus.EventObserver
{
    internal class EventBusRegistrator : IEventBusRegistrator
    {
        private readonly IEventBus _eventBus;

        public EventBusRegistrator(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IDisposable Register<TEvent>(IEventHandler<TEvent> handler) where TEvent : Event
        {
            return _eventBus.Subscribe(handler);
        }
    }
}
