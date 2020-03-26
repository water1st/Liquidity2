using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Service
{
    /// <summary>
    /// 认证
    /// </summary>
    public interface IAuthenticationService
    {
        Task Authorization();

        Task Authorization(string username, string password);
    }
}
