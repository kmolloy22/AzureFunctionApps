namespace AzureFunctionApps.Shared.FunctionApp.Middleware.Interpretation
{
    public interface IInputHandler<in TInput>
    {
        Task<InputHandler<TRequest>> ParseAsync<TRequest>(string handlerName, TInput input) where TRequest : class;
    }
}