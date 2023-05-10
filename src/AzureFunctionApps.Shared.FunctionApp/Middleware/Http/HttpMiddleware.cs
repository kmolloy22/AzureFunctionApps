using AzureFunctionApps.Shared.FunctionApp.Extensions;
using Microsoft.AspNetCore.Http;

namespace AzureFunctionApps.Shared.FunctionApp.Middleware.Http
{
    public interface IHttpMiddleware<TRequest, THandler>
        where TRequest : class
        where THandler : IHttpRequestAction<TRequest>
    {
        Task<HttpResponseMessage> InvokeAsync(HttpRequest request);
    }

    internal class HttpMiddleware<TRequest, THandler> : IHttpMiddleware<TRequest, THandler>
        where TRequest : class
        where THandler : IHttpRequestAction<TRequest>
    {
        private readonly THandler _handler;

        public HttpMiddleware(THandler handler)
        {
            _handler = handler;
        }

        public async Task<HttpResponseMessage> InvokeAsync(HttpRequest request)
        {
            var model = await request.DeserializeBodyAsync<TRequest>();

            HttpResponseMessage response = null;

            try
            {
                response = await _handler.InvokeAsync(model);
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }
    }
}