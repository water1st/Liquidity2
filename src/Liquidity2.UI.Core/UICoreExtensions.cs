using Liquidity2.UI.Core;
using Liquidity2.UI.Core.Builder;
using Liquidity2.UI.Core.Options;
using System;
using Liquidity2.Extensions.Lifecycle.Application;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UICoreExtensions
    {
        public static IUIBuilder AddUICore(this IServiceCollection services, Action<IUICoreBuilder> action)
        {
            services.AddSingleton<IWindowFactory, WindowFactory>();
            services.AddTransient<IWindowCommonBehavior, WindowCommonBehavior>();
            services.AddSingleton<IResourceLoader, ResourceLoader>();
            services.AddApplicationStageObject<LoadResourceLifecycleStageObject>();

            var options = new UIOptions();
            services.AddSingleton(options);
            var builder = new UICoreBuilder(options);
            action(builder);

            return new UIBuilder(services, options);
        }
    }
}
