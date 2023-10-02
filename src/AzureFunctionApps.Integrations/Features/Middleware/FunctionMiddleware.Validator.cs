using AzureFunctionApps.Contracts.ValidationModels;
using FluentValidation;

namespace AzureFunctionApps.Integrations.Features.Middleware
{
    public class FunctionMiddlewareVaidator : AbstractValidator<ValidationRequest>
    {
        public FunctionMiddlewareVaidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Age).NotEmpty().WithMessage("Age is required");
            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("Email is required")
                .EmailAddress()
                    .WithMessage("Email is not valid");
        }
    }
}