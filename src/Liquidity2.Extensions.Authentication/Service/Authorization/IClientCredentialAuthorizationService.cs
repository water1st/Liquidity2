using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.EventBus.EventObserver;

namespace Liquidity2.Extensions.Authentication.Service
{
    public interface IClientCredentialAuthorizationService :
        IAuthenticationService<IdentityAuthentication>,
        IEventHandler<ClientCredentialAuthorizationSuccessEvent>,
        IEventObserver
    {

    }
}
