using Liquidity2.Extensions.Lifecycle.Application;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.EventBus.EventObserver
{
    public class EventObserverRegisterService : ApplicationStageObject
    {
        private readonly IEventBusRegistrator registrator;
        private readonly IEventObserverFactory factory;

        public EventObserverRegisterService(IEventBusRegistrator registrator, IEventObserverFactory factory)
        {
            this.registrator = registrator;
            this.factory = factory;
        }

        protected override int SecondaryStage => 100;

        protected override Task OnStart(CancellationToken token)
        {
            foreach (var observer in factory.Create())
            {
                observer.Subscribe(registrator);
            }

            return Task.CompletedTask;
        }
    }
}
