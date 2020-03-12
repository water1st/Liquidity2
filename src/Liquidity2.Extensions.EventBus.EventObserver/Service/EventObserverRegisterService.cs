using Liquidity2.Extensions.Lifecycle.Application;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.EventBus.EventObserver
{
    public class EventObserverRegisterService : ApplicationStageObject
    {
        private readonly IEventBusRegistrator _registrator;
        private readonly IEventObserverFactory _factory;

        public EventObserverRegisterService(IEventBusRegistrator registrator, IEventObserverFactory factory)
        {
            _registrator = registrator;
            _factory = factory;
        }

        protected override int SecondaryStage => 100;

        protected override Task OnStart(CancellationToken token)
        {
            foreach (var observer in _factory.Create())
            {
                observer.Subscribe(_registrator);
            }

            return Task.CompletedTask;
        }
    }
}
