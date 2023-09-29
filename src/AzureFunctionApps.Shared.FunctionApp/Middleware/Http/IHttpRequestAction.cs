using AzureFunctionApps.Shared.FunctionApp.FunctionAction;

namespace AzureFunctionApps.Shared.FunctionApp.Middleware.Http
{
    public interface IHttpRequestAction<in TRequest> : IFunctionAction<TRequest, HttpResponseMessage>
        where TRequest : class
    {
    }
}