﻿using Liquidity2.Extensions.Authentication;
using Liquidity2.Extensions.Authentication.Factories;
using Liquidity2.Extensions.Authentication.Mapper;
using Liquidity2.Extensions.Authentication.Service;
using Liquidity2.Extensions.EventBus.EventObserver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthenticationServiceExtensions
    {
        public static IAuthorizationBuilder AddAuthentication(this IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationServiceFactory, AuthenticationServiceFactory>();
            services.AddSingleton<IAuthenticationMapper, AuthenticationMapper>();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<ClientCredentialAuthorizationService>();
            services.AddSingleton<IEventObserver>(provider => provider.GetRequiredService<ClientCredentialAuthorizationService>());
            services.AddSingletonNamedService<IAuthorizationService>(AuthorizationType.ClientCredentialAuthentication.ToString(), (provider, key) => provider.GetRequiredService<ClientCredentialAuthorizationService>());
            services.AddSingleton<IClientCredentialAuthorizationService>(provider => provider.GetService<ClientCredentialAuthorizationService>());

            services.AddSingleton<IdentityAuthorizationService>();
            services.AddSingleton<IEventObserver>(provider => provider.GetRequiredService<IdentityAuthorizationService>());
            services.AddSingletonNamedService<IAuthorizationService>(AuthorizationType.IdentityAuthentication.ToString(), (provider, key) => provider.GetRequiredService<IdentityAuthorizationService>());
            services.AddSingleton<IIdentityAuthorizationService>(provider => provider.GetService<IdentityAuthorizationService>());

            return new AuthorizationBuilder(services);
        }
    }
}
