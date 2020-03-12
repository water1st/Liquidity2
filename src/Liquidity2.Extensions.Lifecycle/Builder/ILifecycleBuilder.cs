using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.Lifecycle
{
    public interface ILifecycleBuilder
    {
        IServiceCollection Services { get; }
    }
}
