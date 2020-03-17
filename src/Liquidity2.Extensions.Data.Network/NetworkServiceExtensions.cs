using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.Data.Network
{
    public static class NetworkServiceExtensions
    {
        public static IServiceCollection AddReconnectService(this IServiceCollection services)
        {
            services.AddSingleton<IReconnectService, ReconnectService>();
            return services;
        }
    }
}
