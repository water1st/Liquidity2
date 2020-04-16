using System;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Data.Adapter
{
    public abstract class DataAdapter<TService>
    {
        protected async virtual Task<TResult> Proxy<TResult>(Func<TService, Task<TResult>> callServiceFunc,
            Action<TResult, int> callback, params TService[] services)
        {
            if (callServiceFunc is null)
            {
                throw new ArgumentNullException(nameof(callServiceFunc));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            TResult result = default;

            for (int i = 0; i < services.Length; i++)
            {
                var service = services[i];
                if (service != null)
                {
                    var tempResult = await callServiceFunc(service);
                    if (tempResult != null)
                    {
                        result = tempResult;
                        await Task.Run(() => callback(result, i));
                        return result;
                    }
                }
            }

            return result;
        }
    }
}
