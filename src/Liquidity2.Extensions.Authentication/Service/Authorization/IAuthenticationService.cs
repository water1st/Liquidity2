using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Service
{
    /// <summary>
    /// 授权
    /// </summary>
    public interface IAuthenticationService
    {
        Task<JWT> GetAccessToken();
    }

    public interface IAuthenticationService<TAuthenticationType> : IAuthenticationService
        where TAuthenticationType : IAuthenticationType { }
}
