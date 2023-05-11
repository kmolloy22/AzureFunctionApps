using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AzureFunctionApps.Shared.FunctionApp.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<TModel> DeserializeBodyAsync<TModel>(this HttpRequest request)
            where TModel : class
        {
            try
            {
                using var reader = new StreamReader(request.Body);

                var body = await reader.ReadToEndAsync();

                if (body == null)
                    return null;

                var model = JsonConvert.DeserializeObject<TModel>(body);
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}