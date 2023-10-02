using AzureFunctionApps.Shared.Validation;
using System.Net;

namespace AzureFunctionApps.Shared.FunctionApp.FunctionAction
{
    public abstract class ValidateRequestActionBase<TRequest> : IFunctionAction<TRequest, HttpResponseMessage>
        where TRequest : class
    {
        private readonly IValidationService _validationService;

        public ValidateRequestActionBase(IValidationService validationService)
        {
            _validationService = validationService;
        }

        public async Task<HttpResponseMessage> InvokeAsync(TRequest request)
        {
            var result = _validationService.Validate(request);

            if (result.HasErrorRules)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            return await InternalInvokeAsync(request);
        }

        protected abstract Task<HttpResponseMessage> InternalInvokeAsync(TRequest request);
    }
}