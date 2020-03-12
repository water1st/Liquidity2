namespace Liquidity2.Extensions.Authentication.Client
{
    public class GetClientCredentialAccessTokenResponse
    {
        public string AccessToken { get; set; }
        public string Type { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
    }
}
