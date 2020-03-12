using Liquidity2.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Lifecycle
{
    public partial class LifecycleSubject : ILifecycleSubject
    {
        private readonly ConcurrentDictionary<object, OrderedObserver> _subscribers;
        private readonly ILogger<LifecycleSubject> _logger;

        private int? highStage = null;

        public LifecycleSubject(ILogger<LifecycleSubject> logger)
        {
            this._logger = logger;
            _subscribers = new ConcurrentDictionary<object, OrderedObserver>();
        }

        public virtual async Task OnStart(CancellationToken token)
        {
            if (highStage.HasValue)
            {
                throw new InvalidOperationException("Lifecycle has already been started.");
            }

            try
            {
                foreach (var observerGroup in _subscribers.Values
                    .GroupBy(orderedObserver => orderedObserver.Stage)
                    .OrderBy(group => group.Key))
                {
                    if (token.IsCancellationRequested)
                    {
                        throw new LifecycleCanceledException("Lifecycle start canceled by request");
                    }

                    var stage = observerGroup.Key;
                    highStage = stage;

                    await Task.WhenAll(observerGroup.Select(orderedObserver => orderedObserver.Observer.OnStart(token)));

                    OnStartStageCompleted(stage);
                }
            }
            catch (Exception ex) when (!(ex is LifecycleCanceledException))
            {
                _logger?.LogError("Lifecycle start canceled due to errors at stage {Stage}: {Exception}",
                    highStage, ex);
                throw;
            }
        }

        public virtual async Task OnStop(CancellationToken token)
        {
            if (!highStage.HasValue)
                return;

            var loggedCancellation = false;
            foreach (var observerGroup in _subscribers.Values
                .GroupBy(group => group.Stage)
                .OrderByDescending(group => group.Key)
                .SkipWhile(group => !highStage.Equals(group.Key)))
            {

                if (token.IsCancellationRequested && !loggedCancellation)
                {
                    _logger?.LogWarning("Lifecycle stop operations canceled by request.");
                    loggedCancellation = true;
                }

                var stage = observerGroup.Key;
                highStage = stage;
                try
                {
                    await Task.WhenAll(observerGroup.Select(orderedObserver => orderedObserver.Observer.OnStop(token)));
                }
                catch (Exception ex)
                {
                    _logger?.LogError("Stopping lifecycle encountered an error at stage {Stage}. Continuing to stop. Exception: {Exception}", highStage, ex);
                }

                OnStopStageCompleted(stage);
            }
        }

        public virtual IDisposable Subscribe(string observerName, int stage, ILifecycleObserver observer)
        {
            if (observer is null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            if (highStage.HasValue)
            {
                throw new InvalidOperationException("Lifecycle has already been started.");
            }

            var orderedObserver = new OrderedObserver(stage, observer);
            _subscribers.TryAdd(orderedObserver, orderedObserver);
            return new Disposabler(() => _subscribers.TryRemove(orderedObserver, out _));
        }

        protected virtual void OnStartStageCompleted(int stage) { }
        protected virtual void OnStopStageCompleted(int stage) { }
    }
}
