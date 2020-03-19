using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.WindowPostions
{
    public interface IWindowPostionServiceBuilder
    {
        IServiceCollection Services { get; }
    }
}
