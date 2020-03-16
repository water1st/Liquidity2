using Liquidity2.Extensions.Authentication.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.Authentication.Factories
{
    internal class AuthenticationServiceFactory : IAuthenticationServiceFactory
    {
        private readonly IServiceProvider _provider;

        public AuthenticationServiceFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IAuthorizationService GetAuthenticationService(AuthorizationType type)
        {
            var service = _provider.GetServiceByName<IAuthorizationService>(type.ToString());
            return service;
        }
    }
}
