using Liquidity2.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class KeyedServiceExtensions
    {
        public static IServiceCollection AddTransientKeyedService<TKey, TService>(this IServiceCollection collection, TKey key, Func<IServiceProvider, TKey, TService> factory)
            where TService : class
        {
            return collection.AddSingleton<IKeyedService<TKey, TService>>(sp => new KeyedTransientService<TKey, TService>(key, sp, factory));
        }

        public static IServiceCollection AddTransientKeyedService<TKey, TService, TInstance>(this IServiceCollection collection, TKey key)
            where TInstance : class, TService
            where TService : class
        {
            collection.TryAddTransient<TInstance>();
            return collection.AddSingleton<IKeyedService<TKey, TService>>(sp => new KeyedService<TKey, TService, TInstance>(key, sp));
        }

        public static IServiceCollection AddSingletonKeyedService<TKey, TService>(this IServiceCollection collection, TKey key, Func<IServiceProvider, TKey, TService> factory)
            where TService : class
        {
            return collection.AddSingleton<IKeyedService<TKey, TService>>(sp => new KeyedSingletonService<TKey, TService>(key, sp, factory));
        }

        public static IServiceCollection AddSingletonKeyedService<TKey, TService, TInstance>(this IServiceCollection collection, TKey key)
            where TInstance : class, TService
            where TService : class
        {
            collection.TryAddTransient<TInstance>();
            return collection.AddSingleton<IKeyedService<TKey, TService>>(sp => new KeyedSingletonService<TKey, TService, TInstance>(key, sp));
        }

        public static IServiceCollection AddTransientNamedService<TService>(this IServiceCollection collection, string name, Func<IServiceProvider, string, TService> factory)
            where TService : class
        {
            return collection.AddTransientKeyedService(name, factory);
        }

        public static IServiceCollection AddTransientNamedService<TService, TInstance>(this IServiceCollection collection, string name)
            where TInstance : class, TService
            where TService : class
        {
            return collection.AddTransientKeyedService<string, TService, TInstance>(name);
        }

        public static IServiceCollection AddSingletonNamedService<TService>(this IServiceCollection collection, string name, Func<IServiceProvider, string, TService> factory)
            where TService : class
        {
            return collection.AddSingletonKeyedService(name, factory);
        }

        public static IServiceCollection AddSingletonNamedService<TService, TInstance>(this IServiceCollection collection, string name)
            where TInstance : class, TService
            where TService : class
        {
            return collection.AddSingletonKeyedService<string, TService, TInstance>(name);
        }
    }
}
