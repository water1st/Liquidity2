using System;

namespace Liquidity2.Extensions.DependencyInjection
{
    public interface IKeyedService<TKey, out TService> : IEquatable<TKey>
    {
        TKey Key { get; }
        TService GetService(IServiceProvider services);
    }
}
