using Castle.DynamicProxy;

namespace Liquidity2.UI.Components.Chart
{
    public class NullInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (invocation.InvocationTarget != null)
            {
                invocation.Proceed();
            }
        }
    }
}
