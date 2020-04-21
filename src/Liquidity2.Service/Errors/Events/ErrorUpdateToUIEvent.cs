using Liquidity2.Extensions.EventBus;
using System;

namespace Liquidity2.Data.Client.Market.Errors.Events
{
    public class ErrorUpdateToUIEvent : Event
    {
        public int ErrorCode { get; set; }

        public string Symbol { get; set; }

        public string Operation { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
