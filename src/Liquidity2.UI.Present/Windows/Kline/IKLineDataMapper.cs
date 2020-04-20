using Liquidity2.UI.Components.KLine.Model;

namespace Liquidity2.UI.Present.Windows.Kline
{
    public interface IKLineDataMapper
    {
        KLineTimeSpan MapToTimeSpan(string timeSpan);
    }
}
