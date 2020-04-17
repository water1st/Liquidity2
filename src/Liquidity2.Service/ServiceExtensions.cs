using Liquidity2.Service.Market;
using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Service
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IMarketMapper, MarketMapper>();
            services.AddSingleton<IMarketServer, MarketServer>();
            return services;
        }
    }
}
