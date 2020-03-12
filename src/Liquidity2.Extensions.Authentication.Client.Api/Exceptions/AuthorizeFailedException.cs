using System;
using System.Runtime.Serialization;

namespace Liquidity2.Extensions.Authentication.Client.Api
{
    public class AuthorizeFailedException : Exception
    {
        public AuthorizeFailedException()
        {
        }

        public AuthorizeFailedException(string message) : base(message)
        {
        }

        public AuthorizeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AuthorizeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
