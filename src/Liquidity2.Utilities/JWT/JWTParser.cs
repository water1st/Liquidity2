using System;
using System.Text;

namespace Liquidity2.Utilities.JWT
{
    public static class JWTParser
    {
        public static JWTInfo Parser(string jwt)
        {
            if (string.IsNullOrWhiteSpace(jwt))
            {
                throw new ArgumentException("message", nameof(jwt));
            }

            var token = jwt.Split('.');

            var header = GetFromBase64String(token[0]);
            var playload = GetFromBase64String(token[1]);
            var signature = token[2];


            var result = new JWTInfo
            {
                Header = header,
                Payload = playload,
                Signature = signature
            };

            return result;
        }

        private static string GetFromBase64String(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
            {
                throw new ArgumentException("message", nameof(base64));
            }

            if (!base64.EndsWith("=="))
            {
                base64 += "==";
            }

            var bytes = Convert.FromBase64String(base64);
            var result = Encoding.UTF8.GetString(bytes);

            return result;
        }
    }
}
