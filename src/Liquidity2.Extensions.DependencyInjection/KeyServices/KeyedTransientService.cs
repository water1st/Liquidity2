using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.DependencyInjection
{
    public class KeyedTransientService<TKey, TService> : IKeyedService<TKey, TService>
    where TService : class
    {
        private readonly Func<IServiceProvider, TKey, TService> _factory;


        public KeyedTransientService(TKey key, IServiceProvider services, Func<IServiceProvider, TKey, TService> factory)
        {
            Key = key;
            _factory = factory;
        }

        public TKey Key { get; }

        public TService GetService(IServiceProvider services) => _factory(services, Key);

        public bool Equals(TKey other)
        {
            return Equals(Key, other);
        }
    }


    public class KeyedService<TKey, TService, TInstance> : KeyedTransientService<TKey, TService>
        where TInstance : TService
        where TService : class
    {
        public KeyedService(TKey key, IServiceProvider services) 
            : base(key, services, (sp, k) => sp.GetService<TInstance>())
        {
        }
    }
}
