using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.Authentication.Service;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.Identity.Client;
using Liquidity2.Utilities.JWT;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Identity
{
    public class UserService : IUserService
    {
        private readonly IIdentityClient client;
        private readonly IAuthenticationService authenticationService;

        public UserService(IIdentityClient client,
            IAuthenticationService authenticationService)
        {
            this.client = client;
            this.authenticationService = authenticationService;
        }

        public async Task SynchronizeUserInfo()
        {
            var token = await authenticationService.GetAccessTokenWithIdentity();
            var request = new GetUserInfoRequest { AccessToken = token };
            var response = await client.GetUserInfo(request);

            var user = User.Current;
            user.Name = response.Name;
        }

        public Task Handle(IdentityAuthorizationSuccessEvent @event, CancellationToken token)
        {
            var jwtInfo = JWTParser.Parser(@event.AccessToken);
            var userInfoJson = jwtInfo.Payload;

            var user = User.Current;
            user.Name = "test";
            return Task.CompletedTask;
        }

        public void Subscribe(IEventBus eventBus)
        {
            eventBus.Subscribe(this);
        }

        public async Task UpdateUserInfo()
        {
            var token = await authenticationService.GetAccessTokenWithoutIdentity();
            var user = User.Current;

            var request = new UpdateUserInfoRequest
            {
                AccessToken = token,
                UserName = user.Name
            };

            await client.UpdateUserInfo(request);
        }
    }
}
