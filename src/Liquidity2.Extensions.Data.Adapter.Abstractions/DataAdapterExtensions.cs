using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.Data.Adapter
{
    public static class DataAdapterExtensions
    {
        public static IServiceCollection AddDataProvider<TService, TAdapter>(this IServiceCollection services, Action<IDataAdapterBuilder<TService>> action)
            where TService : class
            where TAdapter : DataAdapter<TService>, TService
        {
            services.AddSingleton<TService, TAdapter>();
            services.AddSingleton<IClientImplementationFactory, ClientImplementationFactory>();

            var builder = new DataAdapterBuilder<TService>(services);
            action(builder);

            return services;
        }
    }
}
