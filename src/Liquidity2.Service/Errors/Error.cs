using System;

namespace Liquidity2.Data.Client.DTO
{
    public class Error
    {
        public int ErrorCode { get; set; }

        public string Symbol { get; set; }

        public string Operation { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
