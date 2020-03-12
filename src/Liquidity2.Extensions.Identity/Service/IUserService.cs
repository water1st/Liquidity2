using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.EventBus.EventObserver;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Identity
{
    public interface IUserService : IEventHandler<IdentityAuthorizationSuccessEvent>, IEventObserver
    {
        Task SynchronizeUserInfo();
        Task UpdateUserInfo();
    }
}
