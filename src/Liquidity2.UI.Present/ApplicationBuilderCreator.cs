using Liquidity2.Extensions.Authentication;
using Liquidity2.Extensions.BackgroundJob;
using Liquidity2.Extensions.Blocker.WPFBlocker;
using Liquidity2.Extensions.Data.Network;
using Liquidity2.Extensions.DependencyInjection;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.Lifecycle;
using Liquidity2.UI.Components;
using Liquidity2.UI.Core;
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
                    .ConfigureClientCredentialOptions(options =>
                    {
                        options.ClientId = "";
                        options.ClientSecret = "";
                        options.IssuerUri = new Uri("");
                        options.Scope = "";
                    })
                    .ConfigureIdentityOptions(options =>
                    {
                        options.ClientId = "";
                        options.ClientSecret = "";
                        options.IssuerUri = new Uri("");
                        options.Scope = "";
                    })
                    .ConfigureTradeOptions(options =>
                    {
                        options.ClientId = "";
                        options.ClientSecret = "";
                        options.IssuerUri = new Uri("");
                        options.Scope = "";
                    });

                    service.AddBlocker();
                    service.AddReconnectService();
                    service.AddBackgroundJobService();
                    service.AddUICore(builder => builder.UseTemplate("BLACK"))
                    .AddUIComponents()
                    .AddUIService()
                    .AddWindows();


                });
        }
    }
}
