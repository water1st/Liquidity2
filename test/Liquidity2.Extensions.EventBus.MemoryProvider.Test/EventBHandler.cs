using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.EventBus.MemoryProvider.Test
{
    public class EventBHandler : IEventHandler<EventB>
    {
        public Task Handle(EventB @event, CancellationToken token)
        {
            @event.Called = true;
            @event.CallCalcel?.Invoke();
            return Task.CompletedTask;
        }
    }
}
