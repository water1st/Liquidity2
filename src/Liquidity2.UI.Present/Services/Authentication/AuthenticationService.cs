using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.EventBus.EventObserver;
using Liquidity2.Extensions.Lifecycle.Application;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows;

namespace Liquidity2.UI.Services
{
    public class AuthenticationService : AuthorizationStageObject,
        IEventHandler<IdentityAuthorizationSuccessEvent>,
        IEventObserver
    {
        private readonly IWindowPresentService presentService;
        private BufferBlock<object> bufferBlock;

        public AuthenticationService(IWindowPresentService presentService)
        {
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            this.presentService = presentService;
        }

        public Task Handle(IdentityAuthorizationSuccessEvent @event, CancellationToken token)
        {
            if (bufferBlock != null)
                bufferBlock.Complete();

            return Task.CompletedTask;
        }

        protected override async Task OnStart(CancellationToken token)
        {
            if (bufferBlock == null)
                bufferBlock = new BufferBlock<object>();

            await presentService.ShowLoginWindow();
            await bufferBlock.Completion;
            bufferBlock = null;
        }

        public void Subscribe(IEventBusRegistrator registrator)
        {
            registrator.Register(this);
        }

    }
}
