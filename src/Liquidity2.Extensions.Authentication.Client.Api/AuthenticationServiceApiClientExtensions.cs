using Liquidity2.Extensions.Authentication;
using Liquidity2.Extensions.Authentication.Client;
using Liquidity2.Extensions.Authentication.Client.Api;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthenticationServiceApiClientExtensions
    {
        public static IAuthorizationBuilder AddOpenidClient(this IAuthorizationBuilder builder)
        {
            var service = builder.Services;
            service.TryAddSingleton<IAuthenticationClientFactory, AuthenticationClientFactory>();

            var authorizationTypes = Enum.GetNames(typeof(AuthorizationType));
            foreach (var type in authorizationTypes)
            {
                AddAuthenticationClient(service, type);
            }

            return builder;
        }

        private static void AddAuthenticationClient(IServiceCollection services, string name)
        {
            services.AddHttpClient(name, (provider, client) =>
            {
                var optionsMonitor = provider.GetService<IOptionsMonitor<AuthorizationOptions>>();
                var options = optionsMonitor.Get(name);
                var uri = options.IssuerUri;
                client.BaseAddress = uri;
            });

            services.AddTransientNamedService<IAuthenticationClient>(name,
                (provider, name) =>
                {
                    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                    var optionsMonitor = provider.GetRequiredService<IOptionsMonitor<AuthorizationOptions>>();
                    var logger = provider.GetService<ILogger<AuthenticationClient>>();
                    var httpClient = httpClientFactory.CreateClient(name);
                    var options = optionsMonitor.Get(name);

                    var client = new AuthenticationClient(httpClient, options, logger);
                    return client;
                });
        }
    }
}
