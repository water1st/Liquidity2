using Liquidity2.Extensions.Lifecycle.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.BackgroundJob
{
    public static class BackgroundJobServiceCollectionExtensions
    {
        public static IServiceCollection AddBackgroundJobService(this IServiceCollection services)
        {
            services.AddApplicationStageObject<IBackgroundJobService, BackgroundJobService>();
            return services;
        }
    }
}
