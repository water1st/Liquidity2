using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Liquidity2.UI.Core
{
    public class WpfApplicationBuilder : IWpfApplicationBuilder
    {
        private IServiceCollection services;
        private IConfiguration configuration;

        public WpfApplicationBuilder()
        {
            services = new ServiceCollection();
            configuration = CreateConfiguration();
            services.AddSingleton(configuration);
        }

        public IWpfApplication Build()
        {
            return new WpfApplication(services.BuildServiceProvider());
        }

        public IWpfApplicationBuilder ConfigureServices(Action<IServiceCollection, IConfiguration> configureServices)
        {
            configureServices(services, configuration);
            return this;
        }
        private IConfiguration CreateConfiguration()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

            return configuration;
        }
    }
}
