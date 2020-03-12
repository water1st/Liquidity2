using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.EventBus
{
    public class EventBusBuilder : IEventBusBuilder
    {
        public EventBusBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
