using Microsoft.Extensions.DependencyInjection;
using System;

namespace Liquidity2.Extensions.Lifecycle.Application
{
    public class ApplicationLifecycleBuilder : IApplicationLifecycleBuilder
    {
        private readonly IServiceCollection _services;

        public ApplicationLifecycleBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public void AddApplicationStageObject<TImplementation>()
            where TImplementation : class, IApplicationLifecycleParticipant
        {
            _services.AddApplicationStageObject<TImplementation>();
        }

        public void AddApplicationStageObject<TImplementation>(Func<IServiceProvider, TImplementation> factory)
            where TImplementation : class, IApplicationLifecycleParticipant
        {
            _services.AddApplicationStageObject(factory);
        }

        public void AddApplicationStageObject<TService, TImplementation>()
            where TImplementation : class, IApplicationLifecycleParticipant, TService
            where TService : class
        {
            _services.AddApplicationStageObject<TService, TImplementation>();
        }

        public void AddApplicationStageObject<TService, TImplementation>(Func<IServiceProvider, TImplementation> factory)
            where TImplementation : class, IApplicationLifecycleParticipant, TService
            where TService : class
        {
            _services.AddApplicationStageObject<TService, TImplementation>(factory);
        }


    }
}
