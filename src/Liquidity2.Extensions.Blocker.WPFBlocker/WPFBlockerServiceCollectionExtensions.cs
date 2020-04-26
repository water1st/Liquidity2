using Liquidity2.Extensions.Blocker;
using Liquidity2.Extensions.Blocker.WPFBlocker;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WPFBlockerServiceCollectionExtensions
    {
        public static IServiceCollection AddBlocker(this IServiceCollection services)
        {
            services.TryAddSingleton<IBlocker, Blocker>();

            return services;
        }
    }
}
