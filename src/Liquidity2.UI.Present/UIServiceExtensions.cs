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

            AddServices(service);
            AddEventObservers(service);
            AddLifecycleStageObjects(service);

            return builder;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IWindowPresentService, WindowPresentService>();
        }

        private static void AddEventObservers(IServiceCollection services)
        {
            services.AddObserverFromExisting<AuthenticationService>();
        }

        private static void AddLifecycleStageObjects(IServiceCollection services)
        {
            services.AddApplicationStageObject<AuthenticationService>();
            services.AddApplicationStageObject<MainInterfacePresentService>();
        }
    }
}
