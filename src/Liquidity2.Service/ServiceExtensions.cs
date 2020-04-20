using Liquidity2.Data.Client.Abstractions.Market;
using Liquidity2.Data.Client.Abstractions.Market.SubscribeModel;
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
            services.AddSingleton<ISubscribeManagerFactory, SubscribeManagerFactory>();
            services.AddSingleton<ISubscribeManager<L2SubscribeModel>, SubscribeManager<L2SubscribeModel>>();
            services.AddSingleton<ISubscribeManager<TOSSubscribeModel>, SubscribeManager<TOSSubscribeModel>>();
            services.AddSingleton<ISubscribeManager<KLineSubscribeModel>, SubscribeManager<KLineSubscribeModel>>();
            services.AddSingleton<ISubscribeManager<TickerSubscribeModel>, SubscribeManager<TickerSubscribeModel>>();

            return services;
        }
    }
}
