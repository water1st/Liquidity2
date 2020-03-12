using Liquidity2.Extensions.Lifecycle;
using Liquidity2.Extensions.Lifecycle.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Liquidity2.UI.Core.Core
{
    public class WpfApplication : IWpfApplication
    {
        private IApplicationLifecycleSubject _applicationLifecycleSubject;

        public WpfApplication(IServiceProvider service)
        {
            Services = service;
            Container.Insecure = service;
            LifecycleRegister();
        }

        public IServiceProvider Services { get; }

        public void Dispose()
        {
            Block(_applicationLifecycleSubject.OnStop());
        }

        public void Start()
        {
            Block(_applicationLifecycleSubject.OnStart());
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

        private void Block(Task task)
        {
            var frame = new DispatcherFrame();
            task.ContinueWith(_ =>
            {
                frame.Continue = false;
            });
            Dispatcher.PushFrame(frame);
        }
    }
}
