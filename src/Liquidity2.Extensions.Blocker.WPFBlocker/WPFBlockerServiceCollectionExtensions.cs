using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Liquidity2.Extensions.Blocker.WPFBlocker
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
