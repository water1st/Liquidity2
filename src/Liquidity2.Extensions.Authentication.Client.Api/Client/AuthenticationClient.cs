using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Client.Api
{
    internal class AuthenticationClient : IAuthenticationClient
    {
        private readonly HttpClient httpClient;
        private readonly AuthorizationOptions options;
        private readonly ILogger logger;

        public AuthenticationClient(HttpClient _httpClient, AuthorizationOptions _options, ILogger<AuthenticationClient> _logger)
        {
            httpClient = _httpClient;
            options = _options;
            logger = _logger;
        }

        public async Task<GetClientCredentialAccessTokenResponse> GetClientCredentialAccessToken()
        {
            var response = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                ClientId = options.ClientId,
                ClientSecret = options.ClientSecret,
                Scope = options.Scope
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
            var response = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                ClientId = options.ClientId,
                ClientSecret = options.ClientSecret,
                Scope = options.Scope,
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

        public async Task<RefreshAccessTokenResponse> RefreshAccessToken(RefreshAccessTokenRequest request)
        {
            var response = await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                ClientId = options.ClientId,
                ClientSecret = options.ClientSecret,
                Scope = options.Scope,
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
            var response = await httpClient.RevokeTokenAsync(new TokenRevocationRequest
            {
                ClientId = options.ClientId,
                ClientSecret = options.ClientSecret,
                Token = request.AccessToken,
            });

            if (response.IsError)
            {
                logger.LogWarning(response.Error, response.Exception);
            }
        }
    }
}
