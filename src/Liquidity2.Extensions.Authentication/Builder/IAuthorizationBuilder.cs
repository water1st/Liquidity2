using Liquidity2.Extensions.Authentication;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IAuthorizationBuilder
    {
        IServiceCollection Services { get; }

        IAuthorizationBuilder ConfigureClientCredentialOptions(Action<AuthorizationOptions> action);
        IAuthorizationBuilder ConfigureIdentityOptions(Action<AuthorizationOptions> action);
        IAuthorizationBuilder ConfigureTradeOptions(Action<AuthorizationOptions> action);
    }
}
