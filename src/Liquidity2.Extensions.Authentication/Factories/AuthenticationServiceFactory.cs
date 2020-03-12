using Liquidity2.Extensions.Authentication.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.Authentication.Factories
{
    internal class AuthenticationServiceFactory : IAuthenticationServiceFactory
    {
        private readonly IServiceProvider provider;

        public AuthenticationServiceFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IAuthenticationService GetAuthenticationService(AuthorizationType type)
        {
            var service = provider.GetServiceByName<IAuthenticationService>(type.ToString());
            return service;
        }
    }
}
