using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.WindowPostions
{
    internal class WindowPostionServiceBuilder : IWindowPostionServiceBuilder
    {
        public WindowPostionServiceBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
