using Liquidity2.Extensions.Authentication;
using Liquidity2.Extensions.BackgroundJob;
using Liquidity2.Extensions.Blocker.WPFBlocker;
using Liquidity2.Extensions.Data.LocalStorage;
using Liquidity2.Extensions.Data.Network;
using Liquidity2.Extensions.DependencyInjection;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.Lifecycle;
using Liquidity2.Extensions.WindowPostions;
using Liquidity2.UI.Components;
using Liquidity2.UI.Core;
using Liquidity2.UI.Templates;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Liquidity2.UI.Present
{
    public class ApplicationBuilderCreator
    {
        public static IWpfApplicationBuilder CreateAppBuilder()
        {
            return new WpfApplicationBuilder()
                .ConfigureServices((service, provider) =>
                {
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
                    //添加交易服务客户端
                    //.AddTradeClient<TTradeClientImp>()
                    .ConfigureIdentityOptions(options =>
                    {
                        options.ClientId = "trades_grpc_client";
                        options.ClientSecret = "abb21609-f9a4-2b28-6622-ec410abe648b";
                        options.IssuerUri = new Uri("http://47.56.95.192:20000/");
                        options.Scope = "profile openid trades_api offline_access";
                    });
                    service.AddLocalStorage(options =>
                    {
                        var fileName = $"{Directory.GetCurrentDirectory()}\\localstorage.db";
                        options.UseSQLite($"Data Source={fileName};Version=3;");
                    });
                    service.AddMemoryCache();

                    service.AddWindowPostionService()
                    .AddWindowPostionsLocalStorageClient();

                    service.AddBlocker();
                    service.AddReconnectService();
                    service.AddBackgroundJobService();
                    service.AddUICore(builder => builder.UseTemplate(TemplateNames.BLACK))
                    .AddUIComponents()
                    .AddUIService()
                    .AddWindows();


                });
        }
    }
}
