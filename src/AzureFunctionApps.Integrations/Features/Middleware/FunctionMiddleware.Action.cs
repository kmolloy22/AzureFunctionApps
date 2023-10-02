using AzureFunctionApps.Contracts.ValidationModels;
using AzureFunctionApps.Shared.FunctionApp.FunctionAction;
using AzureFunctionApps.Shared.Validation;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionApps.Integrations.Features.Middleware
{
    public class FunctionMiddlewareAction : ValidateRequestActionBase<ValidationRequest>
    {
        public FunctionMiddlewareAction(IValidationService validationService) 
            : base(validationService)
        {
        }

        //public async Task<HttpResponseMessage> InvokeAsync(ValidationRequest request)
        //{
        //    return await Task.FromResult(new HttpResponseMessage
        //    {
        //        StatusCode = HttpStatusCode.OK,
        //        Content = new StringContent("Middleware Response", Encoding.UTF8, "text/plain")
        //    });
        //}

        protected override async Task<HttpResponseMessage> InternalInvokeAsync(ValidationRequest request)
        {
            return await Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Middleware Response", Encoding.UTF8, "text/plain")
            });
        }
    }
}