using System;

namespace Liquidity2.Extensions.BackgroundJob
{
    public interface IBackgroundJobService
    {
        void AddJob(string key, Action job);
        void RemoveJob(string key);
    }
}
