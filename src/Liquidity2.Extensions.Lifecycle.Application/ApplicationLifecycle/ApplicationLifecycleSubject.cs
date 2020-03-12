using Microsoft.Extensions.Logging;

namespace Liquidity2.Extensions.Lifecycle.Application
{
    public class ApplicationLifecycleSubject : LifecycleSubject, IApplicationLifecycleSubject
    {
        public ApplicationLifecycleSubject(ILogger<LifecycleSubject> logger) : base(logger)
        {
        }
    }
}
