using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Liquidity2.Extensions.EventBus.EventObserver
{
    public static class EventObserverExtensions
    {
        private static Type interfaceType;
        static EventObserverExtensions()
        {
            interfaceType = typeof(IEventObserver);
        }

        public static void AddObserverFromExisting<TImplementation>(this IServiceCollection services) where TImplementation : class, IEventObserver
        {
            var implementationType = typeof(TImplementation);
            var targetDescriptor = services.FirstOrDefault(des => des.ImplementationType == implementationType);
            if (targetDescriptor != null)
            {
                var descriptor = new ServiceDescriptor(
                    interfaceType,
                    provider => provider.GetRequiredService<TImplementation>(),
                    targetDescriptor.Lifetime);
                services.Add(descriptor);
            }
        }

        public static void AddObserverFromExisting<TService, TImplementation>(this IServiceCollection services) where TImplementation : class, IEventObserver, TService
        {
            var implementationType = typeof(TImplementation);
            var targetDescriptor = services.FirstOrDefault(des => des.ImplementationType == implementationType);
            if (targetDescriptor != null)
            {
                var interfaceDescriptor = new ServiceDescriptor(
                    interfaceType,
                    provider => provider.GetRequiredService<TImplementation>(),
                    targetDescriptor.Lifetime);

                var serviceDescriptor = new ServiceDescriptor(
                    typeof(TService),
                    provider => provider.GetRequiredService<TImplementation>(),
                    targetDescriptor.Lifetime);

                services.Add(interfaceDescriptor);
                services.Add(serviceDescriptor);
            }
        }

        public static void AddEventObserverWithSingleton<TImplementation>(this IServiceCollection services) where TImplementation : class, IEventObserver
        {
            var implementationType = typeof(TImplementation);
            services.AddSingleton(implementationType);
            services.AddSingleton(interfaceType, provider => provider.GetRequiredService(implementationType));
        }

        public static void AddEventObserverWithSingleton<TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> factory) where TImplementation : class, IEventObserver
        {
            var implementationType = typeof(TImplementation);
            services.AddSingleton(factory);
            services.AddSingleton(interfaceType, factory);
        }

        public static void AddEventObserverWithSingleton<TService, TImplementation>(this IServiceCollection services) where TImplementation : class, IEventObserver, TService
        {
            services.AddEventObserverWithSingleton<TImplementation>();
            services.AddSingleton(typeof(TService), provider => provider.GetRequiredService<TImplementation>());
        }

        public static void AddEventObserverWithSingleton<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> factory) where TImplementation : class, IEventObserver, TService
        {
            services.AddEventObserverWithSingleton(factory);
            services.AddSingleton(typeof(TService), factory);
        }
    }
}
