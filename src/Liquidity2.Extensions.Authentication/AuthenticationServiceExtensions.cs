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

            services.AddSingletonNamedService<IAuthorizationService<IdentityAuthInfo>, IdentityAuthenticationService>(AuthorizationType.IdentityAuthentication.ToString());
            services.AddSingletonNamedService<IAuthorizationService<TradeAuthInfo>, TradeAuthenticationService>(AuthorizationType.TradeAuthentication.ToString());
            services.AddSingletonNamedService<IAuthorizationService<ClientCredentialAuthInfo>, ClientCredentialAuthenticationService>(AuthorizationType.ClientCredentialAuthentication.ToString());

            services.AddSingleton<ClientCredentialAuthorizationService>();
            services.AddSingleton<IEventObserver>(provider => provider.GetService<ClientCredentialAuthorizationService>());
            services.AddSingletonNamedService<IAuthorizationService>(AuthorizationType.ClientCredentialAuthentication.ToString(), (provider, key) => provider.GetService<ClientCredentialAuthorizationService>());

            services.AddSingleton<IdentityAuthenticationService>();
            services.AddSingleton<IEventObserver>(provider => provider.GetService<IdentityAuthorizationService>());
            services.AddSingletonNamedService<IAuthorizationService>(AuthorizationType.IdentityAuthentication.ToString(), (provider, key) => provider.GetService<IdentityAuthorizationService>());

            services.AddSingleton<TradeAuthorizationService>();
            services.AddSingleton<IEventObserver>(provider => provider.GetService<TradeAuthorizationService>());
            services.AddSingletonNamedService<IAuthorizationService>(AuthorizationType.TradeAuthentication.ToString(), (provider, key) => provider.GetService<TradeAuthorizationService>());

            return new AuthorizationBuilder(services);
        }
    }
}
