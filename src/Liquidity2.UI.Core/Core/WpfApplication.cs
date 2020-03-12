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
        private IApplicationLifecycleSubject applicationLifecycleSubject;

        public WpfApplication(IServiceProvider service)
        {
            Services = service;
            Container.Insecure = service;
            LifecycleRegister();
        }

        public IServiceProvider Services { get; }

        public void Dispose()
        {
            Block(applicationLifecycleSubject.OnStop());
        }

        public void Start()
        {
            Block(applicationLifecycleSubject.OnStart());
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await applicationLifecycleSubject.OnStart();
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await applicationLifecycleSubject.OnStop();
        }

        private void LifecycleRegister()
        {
            applicationLifecycleSubject = Services.GetService<IApplicationLifecycleSubject>();
            var lifecycleObservers = Services.GetServices<ILifecycleParticipant<IApplicationLifecycle>>();

            foreach (var lifecycleObserver in lifecycleObservers)
            {
                lifecycleObserver.Participate(applicationLifecycleSubject);
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
