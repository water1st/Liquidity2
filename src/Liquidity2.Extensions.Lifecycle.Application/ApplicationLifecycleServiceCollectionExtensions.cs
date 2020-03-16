using Liquidity2.Extensions.Lifecycle.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Liquidity2.Extensions.Lifecycle
{
    public static class ApplicationLifecycleServiceCollectionExtensions
    {
        public static ILifecycleBuilder AddApplicationLifecycle(this ILifecycleBuilder builder, Action<IApplicationLifecycleBuilder> action)
        {
            builder.AddApplicationLifecycle();
            action(new ApplicationLifecycleBuilder(builder.Services));
            return builder;
        }

        public static ILifecycleBuilder AddApplicationLifecycle(this ILifecycleBuilder builder)
        {
            var services = builder.Services;

            services.TryAddSingleton<ApplicationLifecycleSubject>();
            services.TryAddSingleton<IApplicationLifecycleSubject>(provider => provider.GetService<ApplicationLifecycleSubject>());
            services.TryAddSingleton<IApplicationLifecycle>(provider => provider.GetService<ApplicationLifecycleSubject>());

            return builder;
        }
    }
}
