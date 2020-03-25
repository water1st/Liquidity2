using Liquidity2.Extensions.EventBus;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
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
