using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Data.Client.Api
{
    public static class DataApiExtensions
    {
        public static IServiceCollection AddRpcDALServices(this IServiceCollection services)
        {
            return services;
        }

        private static void AddLifecycleStageObjects(IServiceCollection services)
        {
        }
    }
}
