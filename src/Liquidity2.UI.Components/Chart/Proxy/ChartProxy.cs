using Castle.DynamicProxy;
using Liquidity2.UI.Components.Interface;

namespace Liquidity2.UI.Components.Chart
{
    public class ChartProxy<TChart> : IChartProxy<TChart> where TChart : class, IChart
    {
        private readonly IProxyGenerator generator;
        private readonly IInterceptor interceptor;

        public ChartProxy(IProxyGenerator generator, IInterceptor interceptor)
        {
            this.generator = generator;
            this.interceptor = interceptor;
            Chart = generator.CreateInterfaceProxyWithoutTarget<TChart>(interceptor);
        }

        public bool Proxy { get; private set; }

        public TChart Chart { get; private set; }

        public void SetProxy(TChart chart)
        {
            Chart = generator.CreateInterfaceProxyWithTarget(chart, interceptor);
            Proxy = true;
        }
    }
}
