using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.UI.Core
{
    public interface IWpfApplicationBuilder
    {
        IWpfApplicationBuilder ConfigureServices(Action<IServiceCollection, IConfiguration> configureServices);
        IWpfApplication Build();
    }
}
