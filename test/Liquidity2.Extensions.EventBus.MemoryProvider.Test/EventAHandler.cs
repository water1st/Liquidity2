using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.EventBus.MemoryProvider.Test
{
    public class EventAHandler : IEventHandler<EventA>
    {
        public Task Handle(EventA @event, CancellationToken token)
        {
            @event.Called = true;
            return Task.CompletedTask;
        }
    }
}
