using Liquidity2.UI.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.Lifecycle;
using Liquidity2.Extensions.DependencyInjection;
using Liquidity2.Extensions.Authentication;


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
                    .AddEventObservers();
                    //注册生命周期
                    service.AddLifecycle()
                    .AddApplicationLifecycle();
                    //注册依赖注入服务
                    service.AddDependencyInjectionService();
                    //注册授权服务
                    service.AddAuthentication()
                    .AddOpenidClient()
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
                    })
                    ;

                });
        }
    }
}
