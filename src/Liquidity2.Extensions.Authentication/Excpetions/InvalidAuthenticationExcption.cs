using System;
using System.Runtime.Serialization;

namespace Liquidity2.Extensions.Authentication
{
    public class InvalidAuthenticationExcption : Exception
    {
        public InvalidAuthenticationExcption()
        {
        }

        public InvalidAuthenticationExcption(string message) : base(message)
        {
        }

        public InvalidAuthenticationExcption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidAuthenticationExcption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
