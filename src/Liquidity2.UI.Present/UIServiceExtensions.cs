using Liquidity2.Extensions.EventBus.EventObserver;
using Liquidity2.Extensions.Lifecycle.Application;
using Liquidity2.UI.Core.Builder;
using Liquidity2.UI.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UIServiceExtensions
    {
        public static IUIBuilder AddUIService(this IUIBuilder builder)
        {
            var service = builder.Services;

            service.AddSingleton<IWindowPresentService, WindowPresentService>();

            service.AddApplicationStageObject<AuthenticationService>();
            service.AddObserverFromExisting<AuthenticationService>();

            return builder;
        }
    }
}
