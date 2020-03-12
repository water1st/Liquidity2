using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddFromExisting<TService, TImplementation>(this IServiceCollection services) where TImplementation : TService
        {
            var registration = services.FirstOrDefault(service => service.ServiceType == typeof(TImplementation));
            if (registration != null)
            {
                var newRegistration = new ServiceDescriptor(
                    typeof(TService),
                    provider => provider.GetRequiredService<TImplementation>(),
                    registration.Lifetime);
                services.Add(newRegistration);
            }
        }

        public static void TryAddFromExisting<TService, TImplementation>(this IServiceCollection services) where TImplementation : TService
        {
            var providedService = services.FirstOrDefault(service => service.ServiceType == typeof(TService));
            if (providedService == null)
            {
                services.AddFromExisting<TService, TImplementation>();
            }
        }
    }
}
