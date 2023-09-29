using AzureFunctionApps.Shared.FunctionApp.FunctionAction;
using AzureFunctionApps.Shared.FunctionApp.Middleware.Interpretation;
using Microsoft.Extensions.Logging;

namespace AzureFunctionApps.Shared.FunctionApp
{
    public interface IMiddleware<TInput, TInputInterpreter> where TInputInterpreter : class, IInputHandler<TInput>
    {
        Task<TResponse> Invoke<TRequest, TResponse>(TInput input, IFunctionAction<TRequest, TResponse> action, ILogger logger)
            where TRequest : class
            where TResponse : class;
    }

    internal class Middleware<TInput, TInputHandler> : IMiddleware<TInput, TInputHandler>
        where TInputHandler : class, IInputHandler<TInput>
    {
        private readonly IInputHandler<TInput> _inputHandler;

        public Middleware(TInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public async Task<TResponse> Invoke<TRequest, TResponse>(TInput input, IFunctionAction<TRequest, TResponse> action, ILogger logger)
            where TRequest : class
            where TResponse : class
        {
            var handler = await GetInputHandler<TRequest>(action.GetType().Name, input);

            try
            {
                var result = await action.InvokeAsync(handler.Request);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<InputHandler<TRequest>> GetInputHandler<TRequest>(string name, TInput? input)
            where TRequest : class
        {
            try
            {
                var handler = await _inputHandler.ParseAsync<TRequest>(name, input);
                return handler;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}