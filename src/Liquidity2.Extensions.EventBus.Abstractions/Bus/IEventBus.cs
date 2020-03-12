using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.EventBus
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent @event, CancellationToken token) where TEvent : Event;
        Task Publish<TEvent>(IEnumerable<TEvent> events, CancellationToken token) where TEvent : Event;

        IDisposable Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : Event;
        void Unsubscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : Event;
    }
}
