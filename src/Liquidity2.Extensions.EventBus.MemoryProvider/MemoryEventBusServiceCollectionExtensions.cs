using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Liquidity2.Extensions.EventBus
{
    public static class MemoryEventBusServiceCollectionExtensions
    {
        public static IEventBusBuilder AddMemoryProvider(this IEventBusBuilder builder)
        {
            var services = builder.Services;
            services.TryAddSingleton<IEventBus, MemoryEventBus>();
            return builder;
        }
    }
}
