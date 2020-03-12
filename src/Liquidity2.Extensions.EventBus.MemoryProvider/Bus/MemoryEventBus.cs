using Liquidity2.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.EventBus
{
    public partial class MemoryEventBus : IEventBus
    {
        private readonly ILogger logger;
        private readonly ConcurrentDictionary<string, ISet<object>> handlers;

        public MemoryEventBus(ILogger<MemoryEventBus> logger)
        {
            this.logger = logger;
            handlers = new ConcurrentDictionary<string, ISet<object>>();
        }

        public async Task Publish<TEvent>(TEvent @event, CancellationToken token) where TEvent : Event
        {
            var eventData = TypeConverter.Convert(@event, @event.GetType());
            await NotifyObservers(eventData, token);
        }

        public async Task Publish<TEvent>(IEnumerable<TEvent> events, CancellationToken token) where TEvent : Event
        {
            foreach (var @event in events)
            {
                if (token.IsCancellationRequested)
                    break;


                await Publish(@event, token).ConfigureAwait(false);
            }
        }

        public IDisposable Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : Event
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var handlers = this.handlers.GetOrAdd(typeof(TEvent).FullName, new HashSet<object>());
            handlers.Add(handler);
            return new Disposabler(() => Unsubscribe(handler));
        }

        public void Unsubscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : Event
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var handlers = this.handlers.GetOrAdd(typeof(TEvent).FullName, new HashSet<object>());
            handlers.Remove(handler);
        }


        private async Task NotifyObservers<TEvent>(TEvent @event, CancellationToken token) where TEvent : Event
        {
            if (handlers.TryGetValue(@event.GetType().FullName, out ISet<object> observers))
            {
                var deadedObservers = new HashSet<IEventHandler<TEvent>>();

                foreach (var observer in observers)
                {
                    if (token.IsCancellationRequested)
                        break;

                    var handler = observer as IEventHandler<TEvent>;
                    try
                    {
                        await handler.Handle(@event, token).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e, e.Message);
                        deadedObservers.Add(handler);
                    }
                }

                foreach (var deadedObserver in deadedObservers)
                {
                    Unsubscribe(deadedObserver);
                }
            }
        }
    }
}
