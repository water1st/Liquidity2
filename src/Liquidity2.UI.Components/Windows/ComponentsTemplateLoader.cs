using Liquidity2.Extensions.Lifecycle;
using Liquidity2.Extensions.Lifecycle.Application;
using Liquidity2.UI.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.UI.Components
{
    public class ComponentsTemplateLoader : IApplicationLifecycleParticipant
    {
        private readonly IResourceLoader loader;
        private readonly WindowOptions options;

        public ComponentsTemplateLoader(IResourceLoader loader, WindowOptions options)
        {
            this.loader = loader;
            this.options = options;
        }

        public void Participate(IApplicationLifecycle lifecycle)
        {
            lifecycle.Subscribe<ComponentsTemplateLoader>(PresentLifetimeStages.COMPONENTS_TEMPLATE, OnStart);
        }

        private Task OnStart(CancellationToken token)
        {
            loader.Load(options.TemplateFiles);
            return Task.CompletedTask;
        }
    }
}
