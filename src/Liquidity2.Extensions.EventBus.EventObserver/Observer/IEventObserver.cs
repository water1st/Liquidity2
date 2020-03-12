namespace Liquidity2.Extensions.EventBus.EventObserver
{
    public interface IEventObserver
    {
        void Subscribe(IEventBusRegistrator registrator);
    }
}
