using Liquidity2.Extensions.Lifecycle.Application;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.BackgroundJob
{
    internal class BackgroundJobService : AuthorizationStageObject, IBackgroundJobService
    {
        private readonly ConcurrentDictionary<string, CancellationTokenSource> _tokenSources;
        private readonly ILogger _logger;

        public BackgroundJobService(ILogger<BackgroundJobService> logger)
        {
            _tokenSources = new ConcurrentDictionary<string, CancellationTokenSource>();
            _logger = logger;
        }

        public void AddJob(string key, Action job)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            if (_tokenSources.TryAdd(key, tokenSource))
            {
                Task task = new Task(
                    action: job,
                    creationOptions: TaskCreationOptions.LongRunning,
                    cancellationToken: tokenSource.Token);

                task.Start();
                _logger.LogInformation($"{key} is running on background");
            }
            else
            {
                tokenSource.Dispose();
                throw new AddBackgroundJobException("key extis");
            }

        }

        public void RemoveJob(string key)
        {
            if (_tokenSources.TryRemove(key, out var tokenSource))
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
                _logger.LogInformation($"{key} was aborted");
            }
        }

        protected override Task OnStop(CancellationToken token)
        {
            foreach (var source in _tokenSources.Values)
            {
                source.Cancel();
                source.Dispose();
            }

            _tokenSources.Clear();

            return Task.CompletedTask;
        }
    }
}
