using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.Lifecycle
{
    public static class LifecycleServiceCollectionExtensions
    {
        public static ILifecycleBuilder AddLifecycle(this IServiceCollection services)
        {
            return new LifecycleBuilder(services);
        }
    }
}
