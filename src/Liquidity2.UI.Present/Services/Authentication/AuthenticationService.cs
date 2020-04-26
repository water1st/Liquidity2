using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.EventBus.EventObserver;
using Liquidity2.Extensions.Lifecycle.Application;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Liquidity2.UI.Services
{
    public class AuthenticationService : AuthorizationStageObject,
        IEventHandler<IdentityAuthorizationSuccessEvent>,
        IEventObserver
    {
        private readonly IWindowPresentService _presentService;
        private BufferBlock<object> _bufferBlock;

        public AuthenticationService(IWindowPresentService presentService)
        {
            _presentService = presentService;
        }

        public Task Handle(IdentityAuthorizationSuccessEvent @event, CancellationToken token)
        {
            if (_bufferBlock != null)
                _bufferBlock.Complete();

            return Task.CompletedTask;
        }

        protected override async Task OnStart(CancellationToken token)
        {
            if (_bufferBlock == null)
                _bufferBlock = new BufferBlock<object>();

            await _presentService.ShowLoginWindow();
            await _bufferBlock.Completion;
            _bufferBlock = null;
        }

        public void Subscribe(IEventBusRegistrator registrator)
        {
            registrator.Register(this);
        }

    }
}
