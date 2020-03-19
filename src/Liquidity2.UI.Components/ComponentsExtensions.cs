using Liquidity2.UI.Core.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.UI.Components
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
            //builder.AddTemplate("模板名", "路径");
        }

        private static void AddComponents(IServiceCollection services)
        {
            //注册组件如渲染器IRender
        }
    }
}
