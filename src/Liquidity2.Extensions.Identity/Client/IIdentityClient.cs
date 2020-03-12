using System.Threading.Tasks;

namespace Liquidity2.Extensions.Identity.Client
{
    public interface IIdentityClient
    {
        Task<GetUserInfoResponse> GetUserInfo(GetUserInfoRequest request);
        Task UpdateUserInfo(UpdateUserInfoRequest request);
    }
}
