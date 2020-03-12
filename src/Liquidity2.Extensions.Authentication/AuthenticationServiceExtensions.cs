using Liquidity2.Extensions.Authentication.Factories;
using Liquidity2.Extensions.Authentication.Mapper;
using Liquidity2.Extensions.Authentication.Service;
using Liquidity2.Extensions.EventBus.EventObserver;
using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.Authentication
{
    public static class AuthenticationServiceExtensions
    {
        public static IAuthorizationBuilder AddAuthentication(this IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationServiceFactory, AuthenticationServiceFactory>();
            services.AddSingleton<IAuthenticationMapper, AuthenticationMapper>();
            services.AddSingleton<IAuthorizationService, AuthorizationService>();

            services.AddSingleton<ClientCredentialAuthorizationService>();
            services.AddSingleton<IEventObserver>(provider => provider.GetService<ClientCredentialAuthorizationService>());
            services.AddSingletonNamedService<IAuthenticationService>(AuthorizationType.ClientCredentialAuthentication.ToString(), (provider, key) => provider.GetService<ClientCredentialAuthorizationService>());

            services.AddSingleton<IdentityAuthenticationService>();
            services.AddSingleton<IEventObserver>(provider => provider.GetService<IdentityAuthenticationService>());
            services.AddSingletonNamedService<IAuthenticationService>(AuthorizationType.IdentityAuthentication.ToString(), (provider, key) => provider.GetService<IdentityAuthenticationService>());

            return new AuthorizationBuilder(services);
        }
    }
}
