using Liquidity2.Extensions.Blocker;
using Liquidity2.Extensions.Lifecycle;
using Liquidity2.Extensions.Lifecycle.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.UI.Core.Core
{
    public class WpfApplication : IWpfApplication
    {
        private IApplicationLifecycleSubject _applicationLifecycleSubject;
        private readonly IBlocker _blocker;

        public WpfApplication(IServiceProvider service)
        {
            Services = service;
            Container.Insecure = service;
            _blocker = service.GetService<IBlocker>();
            LifecycleRegister();
        }

        public IServiceProvider Services { get; }

        public void Dispose()
        {
            _blocker.Block(_applicationLifecycleSubject.OnStop());
        }

        public void Start()
        {
            _blocker.Block(_applicationLifecycleSubject.OnStart());
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await _applicationLifecycleSubject.OnStart();
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await _applicationLifecycleSubject.OnStop();
        }

        private void LifecycleRegister()
        {
            _applicationLifecycleSubject = Services.GetService<IApplicationLifecycleSubject>();
            var lifecycleObservers = Services.GetServices<ILifecycleParticipant<IApplicationLifecycle>>();

            foreach (var lifecycleObserver in lifecycleObservers)
            {
                lifecycleObserver.Participate(_applicationLifecycleSubject);
            }
        }
    }
}
