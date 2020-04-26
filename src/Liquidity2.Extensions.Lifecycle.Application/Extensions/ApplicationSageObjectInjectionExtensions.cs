using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Liquidity2.Extensions.Lifecycle.Application
{
    public static class ApplicationSageObjectInjectionExtensions
    {
        public static void AddApplicationStageObject<TImplementation>(this IServiceCollection services)
            where TImplementation : class, ILifecycleParticipant<IApplicationLifecycle>
        {
            var implementationType = typeof(TImplementation);
            var descriptor = services.FirstOrDefault(des => des.ImplementationType == implementationType);
            if (descriptor == null)
            {
                services.AddSingleton(implementationType);
            }

            services.AddSingleton(provider => provider.GetRequiredService(implementationType) as ILifecycleParticipant<IApplicationLifecycle>);
        }

        public static void AddApplicationStageObject<TImplementation>(this IServiceCollection services,Func<IServiceProvider, TImplementation> factory)
            where TImplementation : class, ILifecycleParticipant<IApplicationLifecycle>
        {
            var implementationType = typeof(TImplementation);
            var descriptor = services.FirstOrDefault(des => des.ImplementationType == implementationType);
            if (descriptor == null)
            {
                services.AddSingleton(factory);
            }

            services.AddSingleton<ILifecycleParticipant<IApplicationLifecycle>>(factory);
        }

        public static void AddApplicationStageObject<TService, TImplementation>(this IServiceCollection services)
            where TImplementation : class, ILifecycleParticipant<IApplicationLifecycle>, TService
            where TService : class
        {
            services.AddApplicationStageObject<TImplementation>();
            services.AddSingleton<TService>(provider => provider.GetRequiredService<TImplementation>());
        }

        public static void AddApplicationStageObject<TService, TImplementation>(this IServiceCollection services,Func<IServiceProvider, TImplementation> factory)
            where TImplementation : class, ILifecycleParticipant<IApplicationLifecycle>, TService
            where TService : class
        {
            services.AddApplicationStageObject(factory);
            services.AddSingleton<TService>(factory);
        }
    }
}
