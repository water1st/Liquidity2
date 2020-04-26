using Liquidity2.Extensions.Authentication.Client;

namespace Liquidity2.Extensions.Authentication.Mapper
{
    internal class AuthenticationMapper : IAuthenticationMapper
    {
        public JWT Map(RefreshAccessTokenResponse response)
        {
            var jwt = new JWT(response.Type, response.AccessToken, response.RefreshToken, response.ExpiresIn);
            return jwt;
        }
    }
}
