using AzureFunctionApps.Shared.FunctionApp.Middleware.Http;

namespace AzureFunctionApps.Shared.FunctionApp.HttpHelpers
{
    public abstract class ValidateRequestActionBase<TRequest> : IHttpRequestAction<TRequest>
        where TRequest : class
    {
        public async Task<HttpResponseMessage> InvokeAsync(TRequest request)
        {
            return await InternalInvokeAsync(request);
        }

        protected abstract Task<HttpResponseMessage> InternalInvokeAsync(TRequest request);
    }
}