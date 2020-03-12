using System;

namespace Liquidity2.Extensions.Authentication
{
    public struct JWT
    {
        private readonly DateTime _expiresDate;

        public JWT(string type, string accessToken, string refreshToken, int expiresIn) : this()
        {
            _expiresDate = DateTime.Now.AddSeconds(expiresIn);
            Type = type ?? throw new ArgumentNullException(nameof(type));
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
        }

        public string Type { get; }

        public string AccessToken { get; }

        public string RefreshToken { get; }

        public bool IsExpired => DateTime.Now < _expiresDate;

        public static implicit operator string(JWT jwt)
        {
            return jwt.ToString();
        }

        public override string ToString() => $"{Type} {AccessToken}";
    }
}
