﻿using IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Client.Api
{
    internal class AuthenticationClient : IAuthenticationClient
    {
        private readonly HttpClient _httpClient;
        private readonly AuthorizationOptions _options;
        private readonly ILogger _logger;
        private DiscoveryDocumentResponse _discoveryDocument;

        public AuthenticationClient(HttpClient httpClient,
            AuthorizationOptions options,
            ILogger<AuthenticationClient> logger)
        {
            _httpClient = httpClient;
            _options = options;
            _logger = logger;
        }

        public async Task<GetClientCredentialAccessTokenResponse> GetClientCredentialAccessToken()
        {
            var doc = await GetDiscoveryDocument();
            var response = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                ClientId = _options.ClientId,
                ClientSecret = _options.ClientSecret,
                Scope = _options.Scope,
                Address = doc.TokenEndpoint
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
            var doc = await GetDiscoveryDocument();
            var response = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = doc.TokenEndpoint,

                ClientId = _options.ClientId,
                ClientSecret = _options.ClientSecret,
                Scope = _options.Scope,
                UserName = request.UserName,
                Password = request.Password,
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
            var doc = await GetDiscoveryDocument();
            var response = await _httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = doc.TokenEndpoint,

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
            var doc = await GetDiscoveryDocument();
            var response = await _httpClient.RevokeTokenAsync(new TokenRevocationRequest
            {
                Address = doc.RevocationEndpoint,

                ClientId = _options.ClientId,
                ClientSecret = _options.ClientSecret,
                Token = request.AccessToken,
            });

            if (response.IsError)
            {
                _logger.LogWarning(response.Error, response.Exception);
            }
        }

        private async Task<DiscoveryDocumentResponse> GetDiscoveryDocument()
        {
            if (_discoveryDocument == null)
                _discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest { Policy = { RequireHttps = false } });

            return _discoveryDocument;
        }
    }
}
