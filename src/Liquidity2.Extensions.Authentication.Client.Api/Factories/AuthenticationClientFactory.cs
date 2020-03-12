using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.Authentication.Client.Api
{
    public class AuthenticationClientFactory : IAuthenticationClientFactory
    {
        private readonly IServiceProvider service;

        public AuthenticationClientFactory(IServiceProvider service)
        {
            this.service = service;
        }

        public IAuthenticationClient Create(AuthorizationType client)
        {
            var name = client.ToString();
            var result = service.GetServiceByName<IAuthenticationClient>(name);
            return result;
        }
    }
}
