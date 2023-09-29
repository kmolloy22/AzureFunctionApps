using AzureFunctionApps.Contracts.ValidationModels;
using AzureFunctionApps.Shared.FunctionApp;
using AzureFunctionApps.Shared.FunctionApp.FunctionAction;
using AzureFunctionApps.Shared.FunctionApp.Middleware.Interpretation;
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
        private readonly IMiddleware<HttpRequest, HttpRequestInputHandler> _middleware;
        private readonly IFunctionAction<ValidationRequest, HttpResponseMessage> _handler;

        public FunctionMiddleware(
            IMiddleware<HttpRequest, HttpRequestInputHandler> middleware,
            IFunctionAction<ValidationRequest, HttpResponseMessage> handler)
        {
            _middleware = middleware;
            _handler = handler;
        }

        [FunctionName("FunctionMiddleware")]
        public async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger log)
        {
            try
            {
                return await _middleware.Invoke(request, _handler, log);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}