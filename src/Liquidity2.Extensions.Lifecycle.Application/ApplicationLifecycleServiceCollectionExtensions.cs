using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Liquidity2.Extensions.Lifecycle.Application
{
    public static class ApplicationLifecycleServiceCollectionExtensions
    {
        public static ILifecycleBuilder AddApplicationLifecycle(this ILifecycleBuilder builder, Action<IApplicationLifecycleBuilder> action)
        {
            var services = builder.Services;

            services.TryAddSingleton<ApplicationLifecycleSubject>();
            services.TryAddSingleton<IApplicationLifecycleSubject>(provider => provider.GetService<ApplicationLifecycleSubject>());
            services.TryAddSingleton<IApplicationLifecycle>(provider => provider.GetService<ApplicationLifecycleSubject>());

            action(new ApplicationLifecycleBuilder(services));

            return builder;
        }
    }
}
