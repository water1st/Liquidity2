using System;

namespace Liquidity2.Extensions.Lifecycle
{
    public interface ILifecycleObservable : ILifecycleObserver
    {
        IDisposable Subscribe(string observerName, int stage, ILifecycleObserver observer);
    }
}
