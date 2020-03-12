﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Lifecycle
{
    public static partial class LifecycleExtensions
    {
        private static Func<CancellationToken, Task> NoOperation => token => Task.CompletedTask;

        public static IDisposable Subscribe(this ILifecycleObservable observable, string observerName, int stage, Func<CancellationToken, Task> onStart, Func<CancellationToken, Task> onStop)
        {
            if (observable == null) throw new ArgumentNullException(nameof(observable));
            if (onStart == null) throw new ArgumentNullException(nameof(onStart));
            if (onStop == null) throw new ArgumentNullException(nameof(onStop));

            return observable.Subscribe(observerName, stage, new Observer(onStart, onStop));
        }

        public static IDisposable Subscribe(this ILifecycleObservable observable, string observerName, int stage, Func<CancellationToken, Task> onStart)
        {
            return observable.Subscribe(observerName, stage, onStart, NoOperation);
        }

        public static IDisposable Subscribe<TObserver>(this ILifecycleObservable observable, int stage, ILifecycleObserver observer)
        {
            return observable.Subscribe(typeof(TObserver).FullName, stage, observer);
        }

        public static IDisposable Subscribe<TObserver>(this ILifecycleObservable observable, int stage, Func<CancellationToken, Task> onStart, Func<CancellationToken, Task> onStop)
        {
            return observable.Subscribe(typeof(TObserver).FullName, stage, onStart, onStop);
        }

        public static IDisposable Subscribe<TObserver>(this ILifecycleObservable observable, int stage, Func<CancellationToken, Task> onStart)
        {
            return observable.Subscribe<TObserver>(stage, onStart, NoOperation);
        }

        public static IDisposable Subscribe(this ILifecycleObservable observable, int stage, ILifecycleObserver observer)
        {
            return observable.Subscribe(observer.GetType().FullName, stage, observer);
        }

        public static Task OnStart(this ILifecycleObserver observer)
        {
            return observer.OnStart(CancellationToken.None);
        }

        public static Task OnStop(this ILifecycleObserver observer)
        {
            return observer.OnStop(CancellationToken.None);
        }
    }
}
