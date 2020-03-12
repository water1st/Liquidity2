﻿using Liquidity2.Extensions.Authentication.Client;
using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.EventBus;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Service
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IEventBus _eventBus;
        private readonly IAuthenticationClientFactory _factory;

        public AuthorizationService(IEventBus eventBus,
            IAuthenticationClientFactory factory)
        {
            _eventBus = eventBus;
            _factory = factory;
        }

        public async Task Authorization()
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

        public async Task Authorization(string userName, string password)
        {
            var request = new GetPasswordAccessTokenRequest
            {
                UserName = userName,
                Password = password
            };

            try
            {
                var client = _factory.Create(AuthorizationType.IdentityAuthentication);
                var response = await client.GetPasswordAccessToken(request);

                var @event = new IdentityAuthorizationSuccessEvent
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