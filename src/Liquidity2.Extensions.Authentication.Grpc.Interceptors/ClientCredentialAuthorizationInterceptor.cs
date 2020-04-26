using Liquidity2.Extensions.Authentication.Service;
using Liquidity2.Extensions.Blocker;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Grpc.Interceptors
{
    public class ClientCredentialAuthorizationInterceptor : JWTInterceptorBase
    {
        private readonly IClientCredentialAuthorizationService _service;

        public ClientCredentialAuthorizationInterceptor(IClientCredentialAuthorizationService service, IBlocker blocker)
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
