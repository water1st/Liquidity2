using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.Data.Adapter
{
    public interface IDataAdapterBuilder<TService> where TService : class
    {
        void AddTransientDataProvider<TImplementation>(string key) where TImplementation : class, TService;

        void AddTransientDataProvider<TImplementation>(string key, Func<IServiceProvider, TService> factory) where TImplementation : class, TService;

        void AddSingletonDataProvider<TImplementation>(string key) where TImplementation : class, TService;

        void AddSingletonDataProvider<TImplementation>(string key, Func<IServiceProvider, TService> factory) where TImplementation : class, TService;
    }
}
