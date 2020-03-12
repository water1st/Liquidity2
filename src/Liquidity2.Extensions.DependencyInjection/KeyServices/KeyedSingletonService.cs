using System;
using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.DependencyInjection
{
    public class KeyedSingletonService<TKey, TService> : IKeyedService<TKey, TService>
        where TService : class
    {
        private readonly Lazy<TService> _instance;

        public TKey Key { get; }

        public KeyedSingletonService(TKey key, IServiceProvider services, Func<IServiceProvider, TKey, TService> factory)
        {
            Key = key;
            _instance = new Lazy<TService>(() => factory(services, Key));
        }

        public TService GetService(IServiceProvider services) => _instance.Value;

        public bool Equals(TKey other)
        {
            return Equals(Key, other);
        }
    }

    public class KeyedSingletonService<TKey, TService, TInstance> : KeyedSingletonService<TKey, TService>
        where TInstance : TService
        where TService : class
    {
        public KeyedSingletonService(TKey key, IServiceProvider services)
            : base(key, services, (sp, k) => sp.GetService<TInstance>())
        {
        }
    }
}
