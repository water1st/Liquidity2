namespace Liquidity2.Extensions.Lifecycle
{
    public partial class LifecycleSubject
    {
        private class OrderedObserver
        {
            public ILifecycleObserver Observer { get; }

            public int Stage { get; }

            public OrderedObserver(int stage, ILifecycleObserver observer)
            {

                Stage = stage;
                Observer = observer;
            }
        }
    }
}
