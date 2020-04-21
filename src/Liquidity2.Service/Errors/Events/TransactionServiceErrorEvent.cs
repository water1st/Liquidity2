using Liquidity2.Extensions.EventBus;

namespace Liquidity2.Data.Client.Market.Errors.Events
{
    public class TransactionServiceErrorEvent : Event
    {
        public int Code { get; set; }

        public string Symbol { get; set; }

        public string Operation { get; set; }

        public string ErrorMessage { get; set; }
    }
}
