using Liquidity2.Extensions.Data.Network;

namespace Microsoft.Extensions.DependencyInjection
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
