using AzureFunctionApps.Shared.Kernel.Errors.Rules;
using FluentValidation;
using ValidationResult = AzureFunctionApps.Shared.Kernel.Errors.Rules.ValidationResult;


namespace AzureFunctionApps.Shared.Validation
{
    public interface IValidationService
    {
        ValidationResult Validate(object request);
    }

    internal class ValidationService : IValidationService
    {
        private readonly IEnumerable<IValidator> _validators;

        public ValidationService(IEnumerable<IValidator> validators)
        {
            _validators = validators;
        }

        public ValidationResult Validate(object request)
        {
            var errors = _validators
                .Where(_ => _.CanValidateInstancesOfType(request.GetType()))
                .Select(_ =>
                {
                    var context = new ValidationContext<object>(request);
                    return _.Validate(context);
                })
                .SelectMany(MapError)
                .ToArray();

            return ValidationResult.With(errors);
        }

        private ErrorRule[] MapError(FluentValidation.Results.ValidationResult result)
        {
            if (result.IsValid)
                return Array.Empty<ErrorRule>();

            return result.Errors.Select(_ =>
                new ErrorRule
                {
                    Title = _.ErrorMessage,
                    Property = _.PropertyName,
                    AttemptedValue = _.AttemptedValue,
                    Type = _.ErrorCode
                }).ToArray();
        }
    }
}