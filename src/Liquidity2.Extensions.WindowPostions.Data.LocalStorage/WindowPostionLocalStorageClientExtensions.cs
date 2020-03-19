using Liquidity2.Extensions.WindowPostions.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.Extensions.WindowPostions
{
    public static class WindowPostionLocalStorageClientExtensions
    {
        public static void AddWindowPostionsLocalStorageClient(this IWindowPostionServiceBuilder builder)
        {
            builder.Services.AddSingleton<IWindowPostionClient, WindowPostionClient>();
        }
    }
}
