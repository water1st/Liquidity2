using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Client
{
    public interface ITradeAccessClient
    {
        Task<GetTradeAccessTokenResponse> GetTradeAccessToken(GetTradeAccessTokenRequest request);
    }
}
