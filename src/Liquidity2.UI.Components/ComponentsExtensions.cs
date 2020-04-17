using Liquidity2.UI.Components.Templates;
using Liquidity2.UI.Components.UsersControl.GroupButton;
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
            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/GroupWindow_Template.xaml");
            builder.AddTemplate(blackTemplate, $"/{assemblyName};component/Templates/{blackTemplate}/SearchTextBox_Template.xaml");
        }

        private static void AddComponents(IServiceCollection services)
        {
            services.AddTransient<GroupWindow>();
            //注册组件如渲染器IRender
        }
    }
}
