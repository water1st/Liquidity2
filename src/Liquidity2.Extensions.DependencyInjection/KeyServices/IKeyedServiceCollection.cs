using System;
using System.Collections.Generic;

namespace Liquidity2.Extensions.DependencyInjection
{
    public interface IKeyedServiceCollection<TKey, out TService>
        where TService : class
    {
        IEnumerable<IKeyedService<TKey, TService>> GetServices(IServiceProvider services);
        TService GetService(IServiceProvider services, TKey key);
    }
}
