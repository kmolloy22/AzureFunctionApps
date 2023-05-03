using System.ComponentModel.DataAnnotations;

namespace AzureFunctionApps.Shared.Validation
{
    public class ValidationWrapper<T>
    {
        public bool IsValid { get; set; }
        public T Value { get; set; } 

        public IEnumerable<ValidationResult> validationResults { get; set; }

    }
}