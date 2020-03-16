using Liquidity2.Extensions.EventBus.EventObserver;
using Liquidity2.Extensions.Lifecycle.Application;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Liquidity2.Extensions.EventBus
{
    public static class EventObserverServiceCollectionExtensions
    {
        public static IEventBusBuilder AddEventObservers(this IEventBusBuilder builder, Action<IEventObserverBuilder> action)
        {
            builder.AddEventObservers();
            action(new EventObserverBuilder(builder.Services));

            return builder;
        }

        public static IEventBusBuilder AddEventObservers(this IEventBusBuilder builder)
        {
            var services = builder.Services;

            services.TryAddSingleton<IEventBusRegistrator, EventBusRegistrator>();
            services.TryAddSingleton<IEventObserverFactory, EventObserverFactory>();
            services.AddApplicationStageObject<EventObserverRegisterService>();

            return builder;
        }
    }
}
