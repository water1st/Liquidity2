using Liquidity2.Extensions.Authentication.Service;
using Liquidity2.Extensions.Blocker;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Grpc.Interceptors
{
    public class IdentityAuthorizationInterceptor : JWTInterceptorBase
    {
        private readonly IIdentityAuthorizationService _service;

        public IdentityAuthorizationInterceptor(IIdentityAuthorizationService service, IBlocker blocker)
        {
            _service = service;
            Blocker = blocker;
        }

        protected override IBlocker Blocker { get; }

        protected async override Task<JWT> GetAccessToken()
        {
            var jwt = await _service.GetAccessToken();
            return jwt;
        }
    }
}
