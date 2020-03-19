using Microsoft.Extensions.DependencyInjection;

namespace Liquidity2.UI.Core.Builder
{
    public interface IUIBuilder
    {
        IServiceCollection Services { get; }
        void AddTemplate(string name, string path);
    }
}
