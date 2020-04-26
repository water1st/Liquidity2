using Liquidity2.Extensions.Data.LocalStorage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LocalStorageServiceCollectionExtensions
    {
        public static IServiceCollection AddLocalStorage(this IServiceCollection services, Action<IDbConnectionBuilder> action)
        {
            var options = new LocalStorageOptions();
            action(new DbConnectionBuilder(options));
            services.TryAddSingleton(options);
            services.TryAddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            return services;
        }
    }
}
