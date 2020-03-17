using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.Blocker
{
    public static class BlockerServiceCollectionExtensions
    {
        public static IBlockerBuilder AddBlocker(this IServiceCollection services)
        {
            return new BlockerBuilder(services);
        }
    }
}
