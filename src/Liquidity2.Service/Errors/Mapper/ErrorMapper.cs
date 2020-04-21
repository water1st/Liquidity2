using System;
using Liquidity2.Data.Client.DTO;
using Liquidity2.Data.Client.Market.Errors;
using Liquidity2.Data.Client.Market.Errors.Events;

namespace Liquidity2.Service.Errors
{
    public class ErrorMapper : IErrorMapper
    {
        public ErrorPersistentObject Map(Error error)
        {
            var result = new ErrorPersistentObject
            {
                CreateTimestamp = new DateTimeOffset(error.CreateTime).ToUnixTimeSeconds(),
                ErrorCode = error.ErrorCode,
                ErrorMessage = error.ErrorMessage,
                Operation = error.Operation,
                Symbol = error.Symbol
            };
            return result;
        }

        public Error Map(ErrorPersistentObject error)
        {
            return new Error { CreateTime = DateTimeOffset.FromUnixTimeSeconds(error.CreateTimestamp).DateTime, ErrorCode = error.ErrorCode, Symbol = error.Symbol, ErrorMessage = error.ErrorMessage, Operation = error.Operation };
        }

        public ErrorUpdateToUIEvent MapToErrorUpdateEvent(Error error)
        {
            return new ErrorUpdateToUIEvent { CreateTime = error.CreateTime, ErrorCode = error.ErrorCode, Symbol = error.Symbol, ErrorMessage = error.ErrorMessage, Operation = error.Operation };
        }
    }
}
