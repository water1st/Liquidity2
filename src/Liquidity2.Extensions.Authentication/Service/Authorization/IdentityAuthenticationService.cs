using Liquidity2.Extensions.Authentication.Client;
using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.Authentication.Mapper;
using Liquidity2.Extensions.EventBus.EventObserver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Service
{
    public class IdentityAuthorizationService : IIdentityAuthorizationService
    {
        private readonly IAuthenticationClient _client;
        private readonly IAuthenticationMapper _mapper;
        private JWT _token;

        public IdentityAuthorizationService(IAuthenticationClientFactory factory,
            IAuthenticationMapper mapper)
        {
            _client = factory.Create(AuthorizationType.IdentityAuthentication);
            _mapper = mapper;
        }

        public async Task<JWT> GetAccessToken()
        {
            if (_token == null)
                throw new UnauthorizedAccessException("当前用户并未登录");

            if (!_token.IsExpired)
            {
                try
                {
                    await RefreshAccessToken();
                }
                catch
                {
                    throw new AuthenticationExpriedExcption("身份信息已过期");
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

        public Task Handle(IdentityAuthorizationSuccessEvent @event, CancellationToken token)
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
