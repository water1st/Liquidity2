using System.Collections.Generic;

namespace Liquidity2.Extensions.EventBus.EventObserver
{
    public interface IEventObserverFactory
    {
        IEnumerable<IEventObserver> Create();
    }
}
