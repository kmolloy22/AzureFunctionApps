using AzureFunctionApps.Integrations;
using AzureFunctionApps.Shared.FunctionApp;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace AzureFunctionApps.Integrations
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var source = "Integration Examples";
            builder.Services.AddFunctionApps(typeof(Startup).Assembly, source);
        }

        /// <summary>
        /// Used to inject test mocks - is overridden in TestStartup
        /// </summary>
        /// <param name="services"></param>
        protected virtual void RegisterAdditionalServices(IServiceCollection services) 
        {
            // Do not add stuff :: used to inject test mocks
        }
    }
}