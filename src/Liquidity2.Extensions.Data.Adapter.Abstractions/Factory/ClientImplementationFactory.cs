using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.Data.Adapter
{
    internal class ClientImplementationFactory : IClientImplementationFactory
    {
        private readonly IServiceProvider provider;

        public ClientImplementationFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public TClient Create<TClient>(string name) where TClient : class
        {
            return provider.GetRequiredServiceByName<TClient>(name);
        }
    }
}
