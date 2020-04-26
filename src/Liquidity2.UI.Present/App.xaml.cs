using Liquidity2.UI.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Liquidity2.UI.Present
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IWpfApplication application;

        protected override void OnStartup(StartupEventArgs e)
        {
            application = ApplicationBuilderCreator.CreateAppBuilder().Build();
            application.Start();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            application.Dispose();
            base.OnExit(e);
        }
    }
}
