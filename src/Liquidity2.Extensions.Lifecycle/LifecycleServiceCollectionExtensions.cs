using Liquidity2.Extensions.Lifecycle;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LifecycleServiceCollectionExtensions
    {
        public static ILifecycleBuilder AddLifecycle(this IServiceCollection services)
        {
            return new LifecycleBuilder(services);
        }
    }
}
