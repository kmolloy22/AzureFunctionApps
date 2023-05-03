using AzureFunctionApps.Validation.Models;
using AzureFunctionApps.Validation.Shared.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFunctionApps.Validation.Features
{
    public static class FunctionValidation
    {
        [FunctionName("FunctionValidation")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var response = await req.GetBodyAsync<ValidationRequest>();

            if (!response.IsValid)
            {
                return new BadRequestObjectResult($"Invalid input: {string.Join(", ", response.validationResults.Select(x => x.ErrorMessage).ToArray())}");
            }

            return new OkObjectResult(response);
        }
    }
}