using Liquidity2.Extensions.Authentication.Client;

namespace Liquidity2.Extensions.Authentication.Mapper
{
    public interface IAuthenticationMapper
    {
        JWT Map(RefreshAccessTokenResponse response);
    }
}
