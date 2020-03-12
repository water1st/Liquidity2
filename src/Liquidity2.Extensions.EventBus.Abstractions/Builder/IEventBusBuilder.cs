using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.EventBus
{
    public interface IEventBusBuilder
    {
        IServiceCollection Services { get; }
    }
}
