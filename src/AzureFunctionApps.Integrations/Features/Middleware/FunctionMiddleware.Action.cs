using AzureFunctionApps.Contracts.ValidationModels;
using AzureFunctionApps.Shared.FunctionApp.HttpHelpers;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionApps.Integrations.Features.Middleware
{
    public class FunctionMiddlewareAction : ValidateRequestActionBase<ValidationRequest>
    {
        protected override Task<HttpResponseMessage> InternalInvokeAsync(ValidationRequest request)
        {
            var responseMessage = new HttpResponseMessage();
            var content = new StringContent("Middleware Response", Encoding.UTF8, "text/plain");
            responseMessage.Content = content;
            responseMessage.StatusCode = HttpStatusCode.OK;

            return Task.FromResult(responseMessage);
        }
    }
}