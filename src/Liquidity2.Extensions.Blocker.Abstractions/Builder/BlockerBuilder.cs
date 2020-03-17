using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.Blocker
{
    public class BlockerBuilder : IBlockerBuilder
    {
        public BlockerBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
