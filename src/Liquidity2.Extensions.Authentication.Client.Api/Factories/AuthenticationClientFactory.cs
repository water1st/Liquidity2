using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.Authentication.Client.Api
{
    public class AuthenticationClientFactory : IAuthenticationClientFactory
    {
        private readonly IServiceProvider _service;

        public AuthenticationClientFactory(IServiceProvider service)
        {
            _service = service;
        }

        public IAuthenticationClient Create(AuthorizationType client)
        {
            var name = client.ToString();
            var result = _service.GetServiceByName<IAuthenticationClient>(name);
            return result;
        }
    }
}
