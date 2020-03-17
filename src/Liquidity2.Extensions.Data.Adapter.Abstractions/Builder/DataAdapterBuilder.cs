using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.Data.Adapter
{
    public class DataAdapterBuilder<TService> : IDataAdapterBuilder<TService> where TService : class
    {
        private readonly IServiceCollection _services;

        public DataAdapterBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public void AddSingletonDataProvider<TImplementation>(string key) where TImplementation : class, TService
        {
            _services.AddSingletonNamedService<TService, TImplementation>(key);
        }

        public void AddSingletonDataProvider<TImplementation>(string key, Func<IServiceProvider, TService> factory) where TImplementation : class, TService
        {
            _services.AddSingletonNamedService(key, (provider, name) => factory(provider));
        }

        public void AddTransientDataProvider<TImplementation>(string key) where TImplementation : class, TService
        {
            _services.AddTransientNamedService<TService, TImplementation>(key);
        }

        public void AddTransientDataProvider<TImplementation>(string key, Func<IServiceProvider, TService> factory) where TImplementation : class, TService
        {
            _services.AddTransientNamedService(key, (provider, name) => factory(provider));
        }
    }
}
