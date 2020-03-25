using Liquidity2.Extensions.WindowPostions;
using Liquidity2.Extensions.WindowPostions.Client;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WindowPostionLocalStorageClientExtensions
    {
        public static void AddWindowPostionsLocalStorageClient(this IWindowPostionServiceBuilder builder)
        {
            builder.Services.AddSingleton<IWindowPostionClient, WindowPostionClient>();
        }
    }
}
