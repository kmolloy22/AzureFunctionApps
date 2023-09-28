using AzureFunctionApps.Shared.Kernel.Errors.Rules;

namespace AzureFunctionApps.Shared.Kernel.Errors
{
    public class HandlerError
    {
        protected HandlerError(params ErrorRule[] errorRules)
        {
            ErrorRules = errorRules ?? Array.Empty<ErrorRule>();
        }

        public ErrorRule[] ErrorRules { get; }
    }
}