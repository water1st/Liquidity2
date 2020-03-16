using System;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Identity.Client
{
    public class IdentityClient : IIdentityClient
    {
        public IdentityClient()
        {
        }

        public Task<GetUserInfoResponse> GetUserInfo(GetUserInfoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserInfo(UpdateUserInfoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
