using Liquidity2.Extensions.Authentication.Client;
using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.EventBus;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Service
{
    internal class ClientCredentialAuthenticationService : IAuthorizationService<ClientCredentialAuthInfo>
    {
        private readonly IEventBus _eventBus;
        private readonly IAuthenticationClientFactory _factory;

        public ClientCredentialAuthenticationService(IEventBus eventBus,
            IAuthenticationClientFactory factory)
        {
            _eventBus = eventBus;
            _factory = factory;
        }

        public async Task Authorization(ClientCredentialAuthInfo info)
        {
            try
            {
                var client = _factory.Create(AuthorizationType.ClientCredentialAuthentication);
                var response = await client.GetClientCredentialAccessToken();
                var @event = new ClientCredentialAuthorizationSuccessEvent
                {
                    AccessToken = response.AccessToken,
                    RefreshToken = response.RefreshToken,
                    Type = response.Type,
                    ExpiresIn = response.ExpiresIn
                };

                await _eventBus.Publish(@event, CancellationToken.None);
            }
            catch
            {
                throw new InvalidAuthenticationExcption("无效客户端信息");
            }
        }
    }
}
