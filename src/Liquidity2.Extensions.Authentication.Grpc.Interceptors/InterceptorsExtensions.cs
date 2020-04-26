using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.Authentication.Grpc.Interceptors
{
    public static class InterceptorsExtensions
    {
        public static IAuthorizationBuilder AddAuthenticationInterceptors(this IAuthorizationBuilder builder)
        {

            var service = builder.Services;
            service.AddTransient<IdentityAuthorizationInterceptor>();
            service.AddTransient<ClientCredentialAuthorizationInterceptor>();

            return builder;
        }
    }
}
