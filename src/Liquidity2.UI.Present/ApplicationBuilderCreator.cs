using Liquidity2.Data.Client.Api;
using Liquidity2.Extensions.Authentication.Grpc.Interceptors;
using Liquidity2.Service;
using Liquidity2.UI.Core;
using Liquidity2.UI.Templates;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.UI.Present
{
    public class ApplicationBuilderCreator
    {
        public static IWpfApplicationBuilder CreateAppBuilder()
        {
            return new WpfApplicationBuilder()
                .ConfigureServices((service, provider) =>
                {
                    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                    ///注册总线
                    service.AddEventBus()
                    .AddMemoryProvider()
                    //事件观察者重载可使用builder注册事件观察者
                    .AddEventObservers();
                    //注册生命周期
                    service.AddLifecycle()
                    //应用程序生命周期重载可使用builder注册应用程序生命周期对象
                    .AddApplicationLifecycle();
                    //注册依赖注入服务
                    service.AddDependencyInjectionService();
                    //注册授权服务
                    service.AddAuthentication()
                    .AddOpenidClient()
                    .ConfigureIdentityOptions(options =>
                    {
                        //options.ClientId = "test";
                        //options.ClientSecret = "test";
                        //options.IssuerUri = new Uri("test");
                        //options.Scope = "test";
                    }).AddAuthenticationInterceptors();
                    service.AddLocalStorageDAL();

                    service.AddMemoryCache();

                    service.AddWindowPostionService()
                    .AddWindowPostionsLocalStorageClient();

                    service.AddBlocker();
                    service.AddReconnectService();
                    service.AddBackgroundJobService();
                    service.AddUICore(builder => builder.UseTemplate(TemplateNames.TEST))
                    .AddUIComponents()
                    .AddUIService()
                    .AddWindows();

                    service.AddBLL();
                    service.AddRpcDALServices();
                });
        }
    }
}
