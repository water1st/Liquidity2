using Liquidity2.Extensions.Authentication.Client;
using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.EventBus;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Service
{
    public class TradeAuthenticationService : IAuthorizationService<TradeAuthInfo>
    {
        private readonly IEventBus _eventBus;
        private readonly IAuthenticationClientFactory _factory;

        public TradeAuthenticationService(IEventBus eventBus,
            IAuthenticationClientFactory factory)
        {
            _eventBus = eventBus;
            _factory = factory;
        }

        public async Task Authorization(TradeAuthInfo info)
        {
            var request = new GetTradeAccessTokenRequest
            {
                UserName = info.Username,
                Password = info.Password
            };

            try
            {
                var client = _factory.Create(AuthorizationType.TradeAuthentication);
                var response = await client.GetTradeAccessToken(request);

                var @event = new TradeAuthorizationSuccessEvent
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
                throw new InvalidAuthenticationExcption("无效身份信息");
            }
        }
    }
}
