using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AzureFunctionApps.Shared.Validation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddTransient<IValidationService, ValidationService>();

            foreach (var assembly in assemblies)
            {
                AssemblyScanner.FindValidatorsInAssembly(assembly).ForEach(r =>
                {
                    services.Add(new ServiceDescriptor(typeof(IValidator), r.ValidatorType, ServiceLifetime.Transient));
                    services.Add(new ServiceDescriptor(r.InterfaceType, r.ValidatorType, ServiceLifetime.Transient));
                    services.Add(new ServiceDescriptor(r.ValidatorType, r.ValidatorType, ServiceLifetime.Transient));
                });
            }

            return services;
        }
    }
}