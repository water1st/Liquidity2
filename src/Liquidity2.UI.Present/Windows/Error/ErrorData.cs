using System;

namespace Liquidity2.UI.Present.Windows.Error
{
    public class ErrorData
    {
        public ErrorData(DateTime dateTime, string symbol, string operation, int errorCode, string errorMessage)
        {
            CreateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            Symbol = symbol;
            Operation = operation;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public string CreateTime { get; }

        public string Symbol { get; }

        public string Operation { get; }

        public int ErrorCode { get; }

        public string ErrorMessage { get; }
    }
}
