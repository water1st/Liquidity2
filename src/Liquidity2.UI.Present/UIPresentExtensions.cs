using Liquidity2.UI.Core.Builder;
using Liquidity2.UI.Templates;
using Liquidity2.UI.Windows;

namespace Microsoft.Extensions.DependencyInjection
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
            const string blackTemplate = TemplateNames.BLACK;
            const string assemblyName = "Liquidity2.UI.Present";

            builder.AddTemplate("Black", $"/{assemblyName};component/Templates/{blackTemplate}/LoginWindow_Template.xaml");
            builder.AddTemplate("Black", $"/{assemblyName};component/Templates/{blackTemplate}/NavigationWindow_Template.xaml");
        }

        private static void AddWindows(IServiceCollection services)
        {
            services.AddTransient<NavigationWindow>();
            services.AddTransient<LoginWindow>();
        }
    }
}
