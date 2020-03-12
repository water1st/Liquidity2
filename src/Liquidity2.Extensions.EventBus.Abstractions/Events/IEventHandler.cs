using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.EventBus
{
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        Task Handle(TEvent @event, CancellationToken token);
    }
}
