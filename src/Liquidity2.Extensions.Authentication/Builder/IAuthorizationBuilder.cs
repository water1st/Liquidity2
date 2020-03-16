using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.Authentication
{
    public interface IAuthorizationBuilder
    {
        IServiceCollection Services { get; }

        IAuthorizationBuilder ConfigureClientCredentialOptions(Action<AuthorizationOptions> action);
        IAuthorizationBuilder ConfigureIdentityOptions(Action<AuthorizationOptions> action);
        IAuthorizationBuilder ConfigureTradeOptions(Action<AuthorizationOptions> action);
    }
}
