using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.EventBus
{
    public static class EventBusServiceCollectionExtensions
    {
        public static IEventBusBuilder AddEventBus(this IServiceCollection services)
        {
            return new EventBusBuilder(services);
        }
    }
}
