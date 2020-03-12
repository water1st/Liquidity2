using System;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Lifecycle
{
    public static partial class LifecycleExtensions
    {
        private class Observer : ILifecycleObserver
        {
            private readonly Func<CancellationToken, Task> onStart;
            private readonly Func<CancellationToken, Task> onStop;

            public Observer(Func<CancellationToken, Task> onStart, Func<CancellationToken, Task> onStop)
            {
                this.onStart = onStart;
                this.onStop = onStop;
            }

            public Task OnStart(CancellationToken token) => onStart(token);
            public Task OnStop(CancellationToken token) => onStop(token);
        }
    }
}
