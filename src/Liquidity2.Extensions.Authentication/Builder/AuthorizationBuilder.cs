﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.Authentication
{
    internal class AuthorizationBuilder : IAuthorizationBuilder
    {
        public AuthorizationBuilder(IServiceCollection service)
        {
            Services = service;
        }

        public IServiceCollection Services { get; }

        public IAuthorizationBuilder ConfigureClientCredentialOptions(Action<AuthorizationOptions> action)
        {
            Services.Configure(AuthorizationType.ClientCredentialAuthentication.ToString(), action);
            return this;
        }

        public IAuthorizationBuilder ConfigureIdentityOptions(Action<AuthorizationOptions> action)
        {
            Services.Configure(AuthorizationType.IdentityAuthentication.ToString(), action);
            return this;
        }
    }
}
