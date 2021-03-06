﻿using Liquidity2.Extensions.Authentication.Service;

namespace Liquidity2.Extensions.Authentication.Factories
{
    public interface IAuthenticationServiceFactory
    {
        IAuthorizationService GetAuthorizationService(AuthorizationType type);
    }
}
