
namespace Liquidity2.Extensions.Authentication.Client
{
    public interface IAuthenticationClientFactory
    {
        IAuthenticationClient Create(AuthorizationType client);
    }
}
