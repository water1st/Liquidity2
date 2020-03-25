using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Client
{
    internal class NullTradeClient : ITradeAccessClient
    {
        public Task<GetTradeAccessTokenResponse> GetTradeAccessToken(GetTradeAccessTokenRequest request)
        {
            return Task.FromResult<GetTradeAccessTokenResponse>(null);
        }
    }
}
