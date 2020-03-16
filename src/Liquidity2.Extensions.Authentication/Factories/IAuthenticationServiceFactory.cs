using Liquidity2.Extensions.Authentication.Service;

namespace Liquidity2.Extensions.Authentication.Factories
{
    public interface IAuthenticationServiceFactory
    {
        IAuthorizationService GetAuthenticationService(AuthorizationType type);

    }
}
