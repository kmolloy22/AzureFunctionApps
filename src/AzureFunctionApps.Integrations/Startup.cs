using AzureFunctionApps.Integrations;
using AzureFunctionApps.Shared.FunctionApp;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

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
    }
}