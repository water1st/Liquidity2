using Liquidity2.UI.Components.Interface;

namespace Liquidity2.UI.Components.Chart
{
    public interface IChartProxy<TChart> where TChart : class, IChart
    {
        TChart Chart { get; }
        void SetProxy(TChart chart);
        bool Proxy { get; }
    }
}
