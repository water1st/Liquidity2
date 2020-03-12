using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Liquidity2.Extensions.DependencyInjection
{
    public static class DependencyInjectionServiceExtensions
    {
        public static IServiceCollection AddDependencyInjectionService(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(IKeyedServiceCollection<,>), typeof(KeyedServiceCollection<,>));
            return services;
        }
    }
}
