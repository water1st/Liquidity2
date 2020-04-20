using System.Collections.Generic;

namespace Liquidity2.UI.Present.Windows.Entrust
{
    public class EntrustDataMapper : IEntrustDataMapper
    {
        //private static readonly IDictionary<OrderStatus, string> _statusTypies;
        //private static readonly IDictionary<OrderSide, string> _sideTypies;
        //private static readonly IDictionary<Domain.ExchangeType, ExchangeType> _exchangeTypies;

        static EntrustDataMapper()
        {
            //_statusTypies = new Dictionary<OrderStatus, string>();
            //_sideTypies = new Dictionary<OrderSide, string>();
            //_exchangeTypies = new Dictionary<Domain.ExchangeType, ExchangeType>();

            //StatusTypiedMappingInit(_statusTypies);
            //SideTypiedMappingInit(_sideTypies);
            //ExchangeTypiedMappingInit(_exchangeTypies);
        }

        //private static void ExchangeTypiedMappingInit(IDictionary<Domain.ExchangeType, ExchangeType> exchangeTypies)
        //{
        //    exchangeTypies.Add(Domain.ExchangeType.Bitfinex, ExchangeType.Bitfinex);
        //    exchangeTypies.Add(Domain.ExchangeType.Huobi, ExchangeType.Huobi);
        //    exchangeTypies.Add(Domain.ExchangeType.Okex, ExchangeType.Okex);
        //}

        //private static void SideTypiedMappingInit(IDictionary<OrderSide, string> sideTypies)
        //{
        //    sideTypies.Add(OrderSide.LimitBuy, "买");
        //    sideTypies.Add(OrderSide.LimitSell, "卖");
        //}

        //private static void StatusTypiedMappingInit(IDictionary<OrderStatus, string> statusTypies)
        //{
        //    statusTypies.Add(OrderStatus.Canceled, "撤单成功");
        //    statusTypies.Add(OrderStatus.Canceling, "正在撤单");
        //    statusTypies.Add(OrderStatus.CompletelyTransacted, "完全成交");
        //    statusTypies.Add(OrderStatus.Failed, "失败");
        //    statusTypies.Add(OrderStatus.IncompleteTransacted, "部分成交");
        //    statusTypies.Add(OrderStatus.PartiallyFailed, "部分失败");
        //    statusTypies.Add(OrderStatus.Submitting, "正在下单");
        //    statusTypies.Add(OrderStatus.WaitForDeal, "等待成交");
        //    statusTypies.Add(OrderStatus.CanceledAfterPartiallyFilled, "部分撤单");
        //}

        //public EntrustData MapToEntrustData(OrderingData orderData)
        //{
        //    return new EntrustData(
        //        orderData.Order_id,
        //        orderData.Price,
        //        orderData.Amount,
        //        orderData.Pair_symbol.ToUpper(),
        //        _sideTypies[orderData.Order_side],
        //        orderData.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //        _exchangeTypies[orderData.Exchange],
        //        orderData.Volume,
        //        orderData.Price * orderData.Amount
        //        );
        //}

        //public HistoryEntrustData MapToHistoryEntrustData(HistoryOrderData historyOrderData)
        //{
        //    return new HistoryEntrustData(
        //        historyOrderData.Transacted_timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
        //        historyOrderData.Pair_symbol.ToUpper(),
        //        _sideTypies[historyOrderData.Order_type],
        //        historyOrderData.Price,
        //        historyOrderData.Amount,
        //        _statusTypies[historyOrderData.Status],
        //        _exchangeTypies[historyOrderData.Exchange],
        //        historyOrderData.ExFees,
        //        historyOrderData.FilledPriceAvg,
        //        historyOrderData.Order_id
        //        );
        //}
    }
}
