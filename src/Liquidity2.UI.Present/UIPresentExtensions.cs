using Liquidity2.UI.Core.Builder;
using Liquidity2.UI.Templates;

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
            const string blackTemplate = TemplateNames.TEST;
            const string assemblyName = "Liquidity2.UI.Present";

            //builder.AddTemplate("name", "path");
        }

        private static void AddWindows(IServiceCollection services)
        {
            //services.AddTransient<XXWindow>();
        }
    }
}
