using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Service
{
    /// <summary>
    /// 认证
    /// </summary>
    public interface IAuthorizationService
    {
        Task Authorization(string userName, string password);

        Task Authorization();
    }
}
