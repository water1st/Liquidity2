using Liquidity2.Extensions.BackgroundJob;
using Liquidity2.Extensions.Lifecycle.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
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
