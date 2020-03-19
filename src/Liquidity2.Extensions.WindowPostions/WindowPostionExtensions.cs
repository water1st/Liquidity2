using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.WindowPostions
{
    public static class WindowPostionExtensions
    {
        public static IWindowPostionServiceBuilder AddWindowPostionService(this IServiceCollection services)
        {
            services.AddSingleton<IWindowPostionsService, WindowPostionsService>();
            services.AddSingleton<IWindowPostionMapper, WindowPostionMapper>();

            return new WindowPostionServiceBuilder(services);
        }
    }
}
