using Liquidity2.Extensions.WindowPostions;

namespace Microsoft.Extensions.DependencyInjection
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
