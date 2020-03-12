using Liquidity2.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class KeyedServiceCollectionExtensions
    {
        public static TService GetServiceByKey<TKey, TService>(this IServiceProvider services, TKey key)
            where TService : class
        {
            var collection = services.GetService<IKeyedServiceCollection<TKey, TService>>();
            return collection?.GetService(services, key);
        }

        public static TService GetRequiredServiceByKey<TKey, TService>(this IServiceProvider services, TKey key)
            where TService : class
        {
            return services.GetServiceByKey<TKey, TService>(key) ?? throw new KeyNotFoundException(key?.ToString());
        }

        public static TService GetServiceByName<TService>(this IServiceProvider services, string name)
            where TService : class
        {
            return services.GetServiceByKey<string, TService>(name);
        }

        public static TService GetRequiredServiceByName<TService>(this IServiceProvider services, string name)
            where TService : class
        {
            return services.GetRequiredServiceByKey<string, TService>(name);
        }
    }
}
