using System;
using System.Runtime.Serialization;

namespace Liquidity2.Extensions.Authentication
{
    public class AuthenticationExpriedExcption : Exception
    {
        public AuthenticationExpriedExcption()
        {
        }

        public AuthenticationExpriedExcption(string message) : base(message)
        {
        }

        public AuthenticationExpriedExcption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AuthenticationExpriedExcption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
