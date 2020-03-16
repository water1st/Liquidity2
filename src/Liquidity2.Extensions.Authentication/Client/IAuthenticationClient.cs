using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Client
{
    public interface IAuthenticationClient
    {
        Task<GetPasswordAccessTokenResponse> GetPasswordAccessToken(GetPasswordAccessTokenRequest request);

        Task<GetTradeAccessTokenResponse> GetTradeAccessToken(GetTradeAccessTokenRequest request);

        Task<GetClientCredentialAccessTokenResponse> GetClientCredentialAccessToken();

        Task<RefreshAccessTokenResponse> RefreshAccessToken(RefreshAccessTokenRequest request);

        Task RevocationAccessToken(RevocationAccessTokenRequest request);
    }
}
