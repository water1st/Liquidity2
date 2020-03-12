using Liquidity2.Extensions.Authentication.Client;
using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.Authentication.Mapper;
using Liquidity2.Extensions.EventBus.EventObserver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Service
{
    public class ClientCredentialAuthorizationService : IClientCredentialAuthorizationService
    {
        private readonly IAuthenticationClient _client;
        private readonly IAuthenticationMapper _mapper;
        private JWT _token;
        public ClientCredentialAuthorizationService(IAuthenticationClientFactory factory, IAuthenticationMapper mapper)
        {
            _client = factory.Create(AuthorizationType.ClientCredentialAuthentication);
            _mapper = mapper;
        }

        public async Task<JWT> GetAccessToken()
        {
            if (_token == null)
                throw new UnauthorizedAccessException("当前客户端并未认证");

            if (!_token.IsExpired)
            {
                try
                {
                    await RefreshAccessToken();
                }
                catch
                {
                    throw new AuthenticationExpriedExcption("认证信息已过期");
                }
            }

            return _token;
        }

        private async Task RefreshAccessToken()
        {
            var request = new RefreshAccessTokenRequest
            {
                RefreshToken = _token.RefreshToken,
            };

            var response = await _client.RefreshAccessToken(request);

            _token = _mapper.Map(response);
        }

        public Task Handle(ClientCredentialAuthorizationSuccessEvent @event, CancellationToken token)
        {
            _token = new JWT(@event.Type, @event.AccessToken, @event.RefreshToken, @event.ExpiresIn);
            return Task.CompletedTask;
        }

        public void Subscribe(IEventBusRegistrator registrator)
        {
            registrator.Register(this);
        }
    }
}
