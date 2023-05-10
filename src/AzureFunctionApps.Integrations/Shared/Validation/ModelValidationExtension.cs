using AzureFunctionApps.Shared.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AzureFunctionApps.Integrations.Shared.Validation
{
    public static class ModelValidationExtension
    {
        private static ValidationWrapper<T> BuildValidationWrapper<T>(string model)
        {
            ValidationWrapper<T> body = new()
            {
                Value = JsonConvert.DeserializeObject<T>(model)
            };

            var results = new List<ValidationResult>();
            body.IsValid = Validator
                .TryValidateObject(body.Value, new ValidationContext(body.Value, null, null), results, true);
            body.validationResults = results;

            return body;
        }

        public static async Task<ValidationWrapper<T>> GetBodyAsync<T>(this HttpRequest request)
        {
            var bodyString = await request.ReadAsStringAsync();
            return BuildValidationWrapper<T>(bodyString);
        }
    }
}