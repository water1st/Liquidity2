using Liquidity2.UI.Core.Builder;
using Liquidity2.UI.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.UI
{
    public static class UIPresentExtensions
    {
        public static IUIBuilder AddWindows(this IUIBuilder builder)
        {
            AddAddTemplates(builder);
            AddWindows(builder.Services);

            return builder;
        }
        private static void AddAddTemplates(IUIBuilder builder)
        {
            //builder.AddTemplate("模板名", "路径");
        }

        private static void AddWindows(IServiceCollection services)
        {
            services.AddTransient<NavigationWindow>();
        }
    }
}
