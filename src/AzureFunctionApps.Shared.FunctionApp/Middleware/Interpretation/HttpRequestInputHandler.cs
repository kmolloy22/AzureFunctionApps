using AzureFunctionApps.Shared.Kernel.Serialization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AzureFunctionApps.Shared.FunctionApp.Middleware.Interpretation
{
    public class HttpRequestInputHandler : IInputHandler<HttpRequest>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpRequestInputHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<InputHandler<TRequest>> ParseAsync<TRequest>(string handlerName, HttpRequest input) where TRequest : class
        {
            var body = await new StreamReader(input.Body).ReadToEndAsync();

            if (body == null)
                body = "{}";

            var request = Serializer.ToModel<TRequest>(body);

            var info = CreateInfo(input, body);

            return new InputHandler<TRequest>(request, info);
        }

        private InputInfo CreateInfo(HttpRequest input, string body)
        {
            var title = $"{input.Method} {input.Path}";

            var JObject = JsonConvert.DeserializeObject(body);

            var logs = new Dictionary<string, object>
            {
                { "Headers", input.Headers?.ToDictionary(x => x.Key, x => x.Value.ToString()) },
                { "Body", JObject }
            };

            return new InputInfo(title, logs, Array.Empty<string>());
        }
    }
}