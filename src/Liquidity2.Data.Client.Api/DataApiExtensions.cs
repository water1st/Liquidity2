using Liquidity2.Data.Client.Abstractions.Market;
using Liquidity2.Data.Client.Abstractions.Market.SubscribeModel;
using Liquidity2.Data.Client.Api.Queries.Market;
using Liquidity2.Data.Client.Api.Subjects.Market;
using Liquidity2.Extensions.Lifecycle.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Data.Client.Api
{
    public static class DataApiExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IMarketQuery, MarketQuery>();
            services.AddTransient<IMarketModelMapper, MarketModelMapper>();
            services.AddTransient<IMarketQueryMapper, MarketQueryMapper>();
            AddLifecycleStageObjects(services);
            return services;
        }

        private static void AddLifecycleStageObjects(IServiceCollection services)
        {
            services.AddApplicationStageObject<IMarketSubject, MarketSubject>();
            services.AddSingleton<ISubject<L2SubscribeModel>>(provider => provider.GetService<MarketSubject>());
            services.AddSingleton<ISubject<TOSSubscribeModel>>(provider => provider.GetService<MarketSubject>());
            services.AddSingleton<ISubject<KLineSubscribeModel>>(provider => provider.GetService<MarketSubject>()); services.AddSingleton<ISubject<TickerSubscribeModel>>(provider => provider.GetService<MarketSubject>());
        }
    }
}
