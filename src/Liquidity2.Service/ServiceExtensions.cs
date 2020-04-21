using Liquidity2.Extensions.Lifecycle.Application;
using Liquidity2.Service.Errors;
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
            services.AddSingleton<IErrorMapper, ErrorMapper>();
            services.AddApplicationStageObject<IErrorService, ErrorService>();

            services.AddSingleton(typeof(ISubscribeManager<>), typeof(SubscribeManager<>));

            return services;
        }
    }
}
