using System;

namespace Liquidity2.Data.Client.Market.Errors
{
    [Serializable]
    public class ErrorPersistentObject
    {
        public string Id { get; set; }

        public int ErrorCode { get; set; }

        public string Symbol { get; set; }

        public string Operation { get; set; }

        public string ErrorMessage { get; set; }

        public long CreateTimestamp { get; set; }
    }
}
