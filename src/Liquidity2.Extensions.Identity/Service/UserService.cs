using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.Authentication.Service;
using Liquidity2.Extensions.EventBus.EventObserver;
using Liquidity2.Extensions.Identity.Client;
using Liquidity2.Utilities.JWT;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Identity
{
    public class UserService : IUserService
    {
        private readonly IIdentityClient _client;
        private readonly IAuthenticationService _authenticationService;

        public UserService(IIdentityClient client,
            IAuthenticationService authenticationService)
        {
            _client = client;
            _authenticationService = authenticationService;
        }

        public async Task SynchronizeUserInfo()
        {
            var token = await _authenticationService.GetAccessToken();
            var request = new GetUserInfoRequest { AccessToken = token };
            var response = await _client.GetUserInfo(request);

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

        public async Task UpdateUserInfo()
        {
            var token = await _authenticationService.GetAccessToken();
            var user = User.Current;

            var request = new UpdateUserInfoRequest
            {
                AccessToken = token,
                UserName = user.Name
            };

            await _client.UpdateUserInfo(request);
        }

        public void Subscribe(IEventBusRegistrator registrator)
        {
            registrator.Register(this);
        }
    }
}
