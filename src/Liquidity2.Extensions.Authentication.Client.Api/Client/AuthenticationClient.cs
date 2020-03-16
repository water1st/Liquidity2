using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Client.Api
{
    internal class AuthenticationClient : IAuthenticationClient
    {
        private readonly HttpClient _httpClient;
        private readonly AuthorizationOptions _options;
        private readonly ILogger _logger;
        private readonly ITradeAccessClient _tradeAccessClient;

        public AuthenticationClient(HttpClient httpClient,
            AuthorizationOptions options,
            ILogger<AuthenticationClient> logger, ITradeAccessClient tradeAccessClient)
        {
            _httpClient = httpClient;
            _options = options;
            _logger = logger;
            _tradeAccessClient = tradeAccessClient;
        }

        public async Task<GetClientCredentialAccessTokenResponse> GetClientCredentialAccessToken()
        {
            var response = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                ClientId = _options.ClientId,
                ClientSecret = _options.ClientSecret,
                Scope = _options.Scope
            });

            if (response.IsError)
            {
                throw new AuthorizeFailedException(response.Error, response.Exception);
            }

            return new GetClientCredentialAccessTokenResponse
            {
                AccessToken = response.AccessToken,
                Type = response.TokenType,
                ExpiresIn = response.ExpiresIn,
                RefreshToken = response.RefreshToken
            };
        }

        public async Task<GetPasswordAccessTokenResponse> GetPasswordAccessToken(GetPasswordAccessTokenRequest request)
        {
            var response = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                ClientId = _options.ClientId,
                ClientSecret = _options.ClientSecret,
                Scope = _options.Scope,
                UserName = request.UserName,
                Password = request.Password
            });

            if (response.IsError)
            {
                throw new AuthorizeFailedException(response.Error, response.Exception);
            }

            return new GetPasswordAccessTokenResponse
            {
                AccessToken = response.AccessToken,
                Type = response.TokenType,
                ExpiresIn = response.ExpiresIn,
                RefreshToken = response.RefreshToken
            };
        }

        public async Task<GetTradeAccessTokenResponse> GetTradeAccessToken(GetTradeAccessTokenRequest request)
        {
            return await _tradeAccessClient.GetTradeAccessToken(request);
        }

        public async Task<RefreshAccessTokenResponse> RefreshAccessToken(RefreshAccessTokenRequest request)
        {
            var response = await _httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                ClientId = _options.ClientId,
                ClientSecret = _options.ClientSecret,
                Scope = _options.Scope,
                RefreshToken = request.RefreshToken,
            });

            if (response.IsError)
            {
                throw new AuthorizeFailedException(response.Error, response.Exception);
            }

            return new RefreshAccessTokenResponse
            {
                AccessToken = response.AccessToken,
                Type = response.TokenType,
                ExpiresIn = response.ExpiresIn,
                RefreshToken = response.RefreshToken
            };
        }

        public async Task RevocationAccessToken(RevocationAccessTokenRequest request)
        {
            var response = await _httpClient.RevokeTokenAsync(new TokenRevocationRequest
            {
                ClientId = _options.ClientId,
                ClientSecret = _options.ClientSecret,
                Token = request.AccessToken,
            });

            if (response.IsError)
            {
                _logger.LogWarning(response.Error, response.Exception);
            }
        }
    }
}
