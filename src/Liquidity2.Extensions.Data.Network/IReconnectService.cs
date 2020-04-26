using System;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Data.Network
{
    public interface IReconnectService
    {
        Task Reconnect(Func<Task> connect,
            Func<Task> successCallback = null,
            Func<Exception, Task> faildCallback = null);

        Task Reconnect<TConnectResult>(Func<Task<TConnectResult>> connect,
            Func<TConnectResult, Task> successCallback,
            Func<Exception, Task> faildCallback = null);
    }
}
