using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Lifecycle
{
    public interface ILifecycleObserver
    {
        Task OnStart(CancellationToken token);

        Task OnStop(CancellationToken token);
    }
}
