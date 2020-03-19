using Liquidity2.UI.Core.Builder;
using Liquidity2.UI.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.UI.Core
{
    public static class UICoreExtensions
    {
        public static IUIBuilder AddUICore(this IServiceCollection services, Action<IUICoreBuilder> action)
        {
            services.AddSingleton<IWindowFactory, WindowFactory>();
            services.AddTransient<IWindowCommonBehavior, WindowCommonBehavior>();
            var options = new UIOptions();
            services.AddSingleton(options);
            var builder = new UICoreBuilder(options);
            action(builder);

            return new UIBuilder(services, options);
        }
    }
}
