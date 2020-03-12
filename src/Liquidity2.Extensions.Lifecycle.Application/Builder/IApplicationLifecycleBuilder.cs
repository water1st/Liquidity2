using System;

namespace Liquidity2.Extensions.Lifecycle.Application
{
    public interface IApplicationLifecycleBuilder
    {
        void AddApplicationStageObject<TImplementation>() where TImplementation : class, IApplicationLifecycleParticipant;

        void AddApplicationStageObject<TImplementation>(Func<IServiceProvider, TImplementation> factory) where TImplementation : class, IApplicationLifecycleParticipant;

        void AddApplicationStageObject<TService, TImplementation>() where TImplementation : class, IApplicationLifecycleParticipant, TService where TService : class;

        void AddApplicationStageObject<TService, TImplementation>(Func<IServiceProvider, TImplementation> factory) where TImplementation : class, IApplicationLifecycleParticipant, TService where TService : class;

    }
}
