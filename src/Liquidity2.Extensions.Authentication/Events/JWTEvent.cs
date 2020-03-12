using Liquidity2.Extensions.EventBus;

namespace Liquidity2.Extensions.Authentication.Events
{
    public abstract class JWTEvent : Event
    {
        public string Type { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
