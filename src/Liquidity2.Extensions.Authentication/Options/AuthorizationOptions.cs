using System;

namespace Liquidity2.Extensions.Authentication
{
    public class AuthorizationOptions
    {
        public Uri IssuerUri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
    }
}
