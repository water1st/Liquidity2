namespace Liquidity2.UI.Present.Windows.Entrust
{
    public class CancelParameter
    {
        public CancelParameter(string orderId, string symbol)
        {
            OrderId = orderId;
            Symbol = symbol;
        }

        public string OrderId { get; }

        public string Symbol { get; }
    }
}
