using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Service
{
    /// <summary>
    /// 授权
    /// </summary>
    public interface IAuthorizationService
    {
        Task<JWT> GetAccessToken();
    }

    public interface IAuthenticationService<TAuthenticationType> : IAuthorizationService
        where TAuthenticationType : IAuthenticationType { }
}
