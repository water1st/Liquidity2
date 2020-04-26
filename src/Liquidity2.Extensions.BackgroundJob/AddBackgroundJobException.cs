using System;
using System.Runtime.Serialization;

namespace Liquidity2.Extensions.BackgroundJob
{
    public class AddBackgroundJobException : Exception
    {
        public AddBackgroundJobException()
        {
        }

        public AddBackgroundJobException(string message) : base(message)
        {
        }

        public AddBackgroundJobException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddBackgroundJobException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
