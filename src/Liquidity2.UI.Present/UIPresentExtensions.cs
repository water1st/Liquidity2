using Liquidity2.UI.Core.Builder;
using Liquidity2.UI.Present.Windows.Asset;
using Liquidity2.UI.Present.Windows.Entrust;
using Liquidity2.UI.Present.Windows.Kline;
using Liquidity2.UI.Templates;
using Liquidity2.UI.Windows;
using Liquidity2.UI.Windows.TOS;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UIPresentExtensions
    {
        public static IUIBuilder AddWindows(this IUIBuilder builder)
        {
            AddTemplates(builder);
            AddWindows(builder.Services);

            return builder;
        }
        private static void AddTemplates(IUIBuilder builder)
        {
            const string blackTemplate = TemplateNames.BLACK;
            const string assemblyName = "Liquidity2.UI.Present";

            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/LoginWindow_Template.xaml");
            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/NavigationWindow_Template.xaml");
            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/TOSWindow_Template.xaml");
            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/OrderWindow_Template.xaml");
            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/KLineWindow_Template.xaml");
            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/EntrustWindow_Template.xaml");
            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/AssetWindow_Template.xaml");
        }

        private static void AddWindows(IServiceCollection services)
        {
            services.AddTransient<NavigationWindow>();
            services.AddTransient<LoginWindow>();
            services.AddTransient<TOSWindow>();
            services.AddTransient<ITOSWindowDataMapper, TOSWindowDataMapper>();
            services.AddTransient<OrderWindow>();
            services.AddTransient<KLineWindow>();
            services.AddTransient<IKLineDataMapper, KLineDataMapper>();
            services.AddTransient<EntrustWindow>();
            services.AddTransient<IEntrustDataMapper, EntrustDataMapper>();
            services.AddTransient<AssetWindow>();
            services.AddTransient<IAssetDataMapper, AssetDataMapper>();
        }
    }
}
