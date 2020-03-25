using Liquidity2.Extensions.Lifecycle.Application;
using Liquidity2.UI.Core.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.UI.Core
{
    public class LoadResourceLifecycleStageObject : UICoreStageObject
    {
        private readonly IResourceLoader _loader;
        private readonly UIOptions _options;

        public LoadResourceLifecycleStageObject(IResourceLoader loader, UIOptions options)
        {
            _loader = loader;
            _options = options;
        }

        protected override Task OnStart(CancellationToken token)
        {
            _loader.Load(_options.TemplateFiles);
            return Task.CompletedTask;
        }
    }
}
