using Liquidity2.UI.Core.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.UI.Core.Builder
{
    internal class UIBuilder : IUIBuilder
    {
        private readonly UIOptions options;

        public UIBuilder(IServiceCollection services, UIOptions options)
        {
            Services = services;
            this.options = options;
        }

        public IServiceCollection Services { get; }

        public void AddTemplate(string name, string path)
        {
            options.AddTemplateFile(name, path);
        }
    }
}
