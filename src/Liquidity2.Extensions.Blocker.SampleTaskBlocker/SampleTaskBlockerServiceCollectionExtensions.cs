using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Liquidity2.Extensions.Blocker.SampleTaskBlocker
{
    public static class SampleTaskBlockerServiceCollectionExtensions
    {
        public static IServiceCollection AddBlocker(this IServiceCollection services)
        {
            services.TryAddSingleton<IBlocker, Blocker>();

            return services;
        }
    }
}
