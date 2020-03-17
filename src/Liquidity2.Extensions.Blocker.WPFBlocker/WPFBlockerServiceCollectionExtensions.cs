using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Liquidity2.Extensions.Blocker.WPFBlocker
{
    public static class WPFBlockerServiceCollectionExtensions
    {
        public static void AddWPFBlockerProvider(this IBlockerBuilder builder)
        {
            builder.Services.TryAddSingleton<IBlocker, Blocker>();
        }
    }
}
