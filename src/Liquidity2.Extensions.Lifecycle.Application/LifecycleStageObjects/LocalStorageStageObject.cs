﻿using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Lifecycle.Application
{
    public abstract class LocalStorageStageObject : LifecycleStageObject, IApplicationLifecycleParticipant
    {
        public void Participate(IApplicationLifecycle lifecycle)
        {
            var name = GetType().FullName;
            lifecycle.Subscribe(name, ApplicationLifecycleStages.LOCAL_STORAGE + SecondaryStage, OnStart, OnStop);
        }

        protected virtual Task OnStart(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnStop(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}
