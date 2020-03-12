using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.Lifecycle
{
    public class LifecycleBuilder : ILifecycleBuilder
    {
        public LifecycleBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
