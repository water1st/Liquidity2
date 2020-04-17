using Liquidity2.Data.Client.Abstractions.Market;
using Liquidity2.Service.Market;
using Liquidity2.Service.SubscribeManager;
using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Service
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IMarketMapper, MarketMapper>();
            services.AddSingleton<IMarketService, MarketService>();
            services.AddSingleton<ISubscribeManager<IMarketSubject>, MarketSubscribeManager>();
            return services;
        }
    }
}
