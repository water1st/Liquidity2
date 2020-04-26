using System;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Data.Network
{
    internal class ReconnectService : IReconnectService
    {
        private readonly TimeSpan[] timeSpans;

        public ReconnectService()
        {
            timeSpans = new TimeSpan[] {
                //5分钟间隔,重试2次
                TimeSpan.FromMinutes(5),
                //1分钟间隔，重试4次
                TimeSpan.FromMinutes(1),
                //10秒间隔，重试6次
                TimeSpan.FromSeconds(10),
                //1秒间隔，重试8次
                TimeSpan.FromSeconds(1),
                //1毫秒间隔，重试10次
                TimeSpan.FromMilliseconds(1)
            };
        }


        public async Task Reconnect(Func<Task> connect, Func<Task> successCallback = null, Func<Exception, Task> faildCallback = null)
        {
            if (connect is null)
            {
                throw new ArgumentNullException(nameof(connect));
            }

            Exception exception = null;
            for (var i = timeSpans.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < (i + 1) * 2; j++)
                {
                    try
                    {
                        await connect();
                        if (successCallback != null)
                            await successCallback();
                        return;
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                        await Task.Delay(timeSpans[i]);
                    }
                }
            }

            if (faildCallback != null)
                await faildCallback(exception);
        }
        public async Task Reconnect<TConnectResult>(Func<Task<TConnectResult>> connect, Func<TConnectResult, Task> successCallback, Func<Exception, Task> faildCallback = null)
        {
            if (connect is null)
            {
                throw new ArgumentNullException(nameof(connect));
            }

            Exception exception = null;
            for (var i = timeSpans.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < (i + 1) * 2; j++)
                {
                    try
                    {
                        var result = await connect();
                        if (successCallback != null)
                            await successCallback(result);
                        return;
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                        await Task.Delay(timeSpans[i]);
                    }
                }
            }

            if (faildCallback != null)
                await faildCallback(exception);
        }
    }
}
