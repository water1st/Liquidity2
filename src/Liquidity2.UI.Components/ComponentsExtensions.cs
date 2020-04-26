using Castle.DynamicProxy;
using Liquidity2.UI.Components.Chart;
using Liquidity2.UI.Components.Chart.Render;
using Liquidity2.UI.Components.Interface;
using Liquidity2.UI.Components.KLine;
using Liquidity2.UI.Components.KLine.Renderer;
using Liquidity2.UI.Components.Renderer;
using Liquidity2.UI.Components.Templates;
using Liquidity2.UI.Components.UsersControl.GroupButton;
using Liquidity2.UI.Components.Volume;
using Liquidity2.UI.Components.Volume.Renderer;
using Liquidity2.UI.Core.Builder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ComponentsExtensions
    {
        public static IUIBuilder AddUIComponents(this IUIBuilder builder)
        {
            AddAddTemplates(builder);
            AddComponents(builder.Services);

            return builder;
        }

        private static void AddAddTemplates(IUIBuilder builder)
        {
            const string blackTemplate = TemplateNames.BLACK;
            const string assemblyName = "Liquidity2.UI.Components";

            //builder.AddTemplate("模板名", "路径");
            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/Window_Template.xaml");
            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/GroupWindow_Template.xaml");
            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/SearchTextBox_Template.xaml");
            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/TabControl_Template.xaml");
        }

        private static void AddComponents(IServiceCollection services)
        {
            services.AddTransient<GroupWindow>();
            //注册组件如渲染器IRender
            services.AddSingleton<IRendererFactory, RendererFactory>();
            services.AddSingleton<IRender, AxisLineRenderer>();
            services.AddSingleton<IRender, TimeLabelRenderer>();
            services.AddSingleton<ITimeLabelMapper, TimeLabelMapper>();
            services.AddSingleton<IRender, AxisLabelRenderer>();
            services.AddSingleton<IRender, KLineRenderer>();
            services.AddTransient<IRender, GuideLineRenderer>();
            services.AddSingleton<IRender, LineRender>();
            services.AddSingleton<IRender, KlineInfoRender>();
            services.AddTransient<IChart, KLine>();

            services.AddTransient<IRender, VolumeRenderer>();
            services.AddTransient<IChart, Volume>();

            services.AddSingleton<IProxyGenerator, ProxyGenerator>();
            services.AddSingleton<IInterceptor, NullInterceptor>();
            services.AddTransient(typeof(IChartProxy<>), typeof(ChartProxy<>));
        }
    }
}
