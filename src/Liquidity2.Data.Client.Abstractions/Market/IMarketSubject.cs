using Liquidity2.Data.Client.Abstractions.Market.SubscribeModel;

namespace Liquidity2.Data.Client.Abstractions.Market
{
    public interface IMarketSubject: ISubject<L2SubscribeModel>,ISubject<TOSSubscribeModel>,ISubject<KLineSubscribeModel>,ISubject<TickerSubscribeModel>
    {
    }
}
