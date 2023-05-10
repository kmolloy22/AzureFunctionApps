namespace AzureFunctionApps.Shared.FunctionApp.Middleware
{
    public interface IFunctionAction<in TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        Task<TResponse> InvokeAsync(TRequest request);
    }
}