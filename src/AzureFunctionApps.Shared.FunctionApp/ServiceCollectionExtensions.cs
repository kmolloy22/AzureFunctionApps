using AzureFunctionApps.Shared.FunctionApp.Middleware;
using AzureFunctionApps.Shared.FunctionApp.Middleware.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AzureFunctionApps.Shared.FunctionApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFunctionApps(this IServiceCollection services, Assembly assembly, string source)
        {
            // Middleware
            services.AddScoped(typeof(IHttpMiddleware<,>), typeof(HttpMiddleware<,>));

            AddMultipleGeneric(services, typeof(IFunctionAction<,>), ServiceLifetime.Transient, assembly);
            services.AddMultipleNonGeneric(typeof(IFunctionAction<,>), ServiceLifetime.Transient, assembly);
            return services;
        }

        private static void AddMultipleGeneric(this IServiceCollection services, Type interfaceType, ServiceLifetime lifetime, Assembly assembly)
        {
            foreach (var implementationType in assembly.GetTypes().Where(type => !type.IsAbstract && !type.IsInterface && type.IsClass))
            {
                foreach (var i in implementationType.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType))
                {
                    var genericInterfaceType = interfaceType.MakeGenericType(i.GetGenericArguments());
                    services.Add(new ServiceDescriptor(genericInterfaceType, implementationType, lifetime));
                    // AsSelf
                    services.Add(new ServiceDescriptor(implementationType, implementationType, lifetime));
                }
            }
        }

        private static void AddMultipleNonGeneric(this IServiceCollection services, Type genericBaseInterfaceType, ServiceLifetime lifetime, Assembly assembly)
        {
            var implementingTypes = assembly.GetTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface && type.IsClass)
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericBaseInterfaceType));

            foreach (var implementationType in implementingTypes)
            {
                // Implementing type must have a non-generic interface, implementing the generic interface
                var nonGeneric = implementationType.GetInterfaces()
                    .Where(i => i.IsGenericType == false)
                    .Where(nonGenericInterface => nonGenericInterface.GetInterfaces()
                        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericBaseInterfaceType));

                var type = nonGeneric.FirstOrDefault();

                if (type == null)
                    continue;

                services.Add(new ServiceDescriptor(type, implementationType, lifetime));
            }
        }
    }
}