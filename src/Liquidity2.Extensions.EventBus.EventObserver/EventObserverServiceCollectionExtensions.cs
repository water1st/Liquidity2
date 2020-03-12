using Liquidity2.Extensions.Lifecycle.Application;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Liquidity2.Extensions.EventBus.EventObserver
{
    public static class EventObserverServiceCollectionExtensions
    {
        public static IEventBusBuilder AddEventObservers(this IEventBusBuilder builder, Action<IEventObserverBuilder> action)
        {
            var services = builder.Services;

            services.TryAddSingleton<IEventBusRegistrator, EventBusRegistrator>();
            services.TryAddSingleton<IEventObserverFactory, EventObserverFactory>();
            services.AddApplicationStageObject<EventObserverRegisterService>();

            action(new EventObserverBuilder(services));

            return builder;
        }
    }
}
