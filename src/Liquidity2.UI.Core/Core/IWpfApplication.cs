using System;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.UI.Core
{
    public interface IWpfApplication : IDisposable
    {
        IServiceProvider Services { get; }

        void Start();
        Task StartAsync(CancellationToken cancellationToken = default);
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}
