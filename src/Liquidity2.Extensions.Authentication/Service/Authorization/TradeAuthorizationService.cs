using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.EventBus.EventObserver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Service
{
    internal class TradeAuthorizationService : ITradeAuthorizationService
    {
        private JWT _token;
        public Task<JWT> GetAccessToken()
        {
            if (_token == null)
                throw new UnauthorizedAccessException("二级密码错误");

            if (!_token.IsExpired)
            {
                throw new AuthenticationExpriedExcption("授权信息已过期");
            }

            return Task.FromResult(_token);
        }

        public Task Handle(TradeAuthorizationSuccessEvent @event, CancellationToken token)
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
