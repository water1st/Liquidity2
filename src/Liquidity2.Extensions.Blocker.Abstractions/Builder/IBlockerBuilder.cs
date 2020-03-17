using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.Blocker
{
    public interface IBlockerBuilder
    {
        IServiceCollection Services { get; }
    }
}
