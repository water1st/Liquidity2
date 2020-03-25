using Liquidity2.Extensions.EventBus;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EventBusServiceCollectionExtensions
    {
        public static IEventBusBuilder AddEventBus(this IServiceCollection services)
        {
            return new EventBusBuilder(services);
        }
    }
}
