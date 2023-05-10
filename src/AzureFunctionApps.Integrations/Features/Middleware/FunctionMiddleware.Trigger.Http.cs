using AzureFunctionApps.Contracts.ValidationModels;
using AzureFunctionApps.Shared.FunctionApp.Middleware.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureFunctionApps.Integrations.Features.Middleware
{
    public class FunctionMiddleware
    {
        private readonly IHttpMiddleware<ValidationRequest, FunctionMiddlewareAction> _middleware;

        public FunctionMiddleware(IHttpMiddleware<ValidationRequest, FunctionMiddlewareAction> middleware)
        {
            _middleware = middleware;
        }

        [FunctionName("FunctionMiddleware")]
        public async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger log)
        {
            try
            {
                return await _middleware.InvokeAsync(request);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}