using AzureFunctionApps.Integrations;
using AzureFunctionApps.Shared.FunctionApp;
using AzureFunctionApps.Shared.FunctionApp.Middleware.Interpretation;
using AzureFunctionApps.Shared.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace AzureFunctionApps.Integrations
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddInputInterpreter<HttpRequest, HttpRequestInputHandler>();

            builder.Services.AddMiddleware(typeof(Startup).Assembly);

            builder.Services.AddValidators(typeof(Startup).Assembly);
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