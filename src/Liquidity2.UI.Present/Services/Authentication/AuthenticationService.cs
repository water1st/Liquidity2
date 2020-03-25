using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.EventBus.EventObserver;
using Liquidity2.Extensions.Lifecycle.Application;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Liquidity2.UI.Services
{
    public class AuthenticationService : AuthorizationStageObject,
        IEventHandler<IdentityAuthorizationSuccessEvent>,
        IEventObserver
    {
        private readonly DispatcherFrame frame;
        private readonly IWindowPresentService presentService;

        public AuthenticationService(IWindowPresentService presentService)
        {
            frame = new DispatcherFrame();
            this.presentService = presentService;
        }

        public Task Handle(IdentityAuthorizationSuccessEvent @event, CancellationToken token)
        {
            frame.Continue = true;
            return Task.CompletedTask;
        }

        protected override async Task OnStart(CancellationToken token)
        {
            await presentService.ShowLoginWindow();
            frame.Continue = false;
            Dispatcher.PushFrame(frame);
        }

        public void Subscribe(IEventBusRegistrator registrator)
        {
            registrator.Register(this);
        }

    }
}
