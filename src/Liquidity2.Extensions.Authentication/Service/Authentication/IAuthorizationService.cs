using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Service
{
    /// <summary>
    /// 认证
    /// </summary>
    public interface IAuthorizationService<TAuthInfo> where TAuthInfo : IAuthInfo
    {
        Task Authorization(TAuthInfo info);
    }
}
