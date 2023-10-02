using System.Text;

namespace AzureFunctionApps.Shared.Kernel.Errors.Rules
{
    public class ValidationResult
    {
        //public ValidationResult()
        //{
        //    Errors = new List<ErrorRule>();
        //}

        private ValidationResult(ErrorRule[] errors)
        {
            if (errors == null)
                Errors = Array.Empty<ErrorRule>();

            Errors = errors;
        }

        public bool IsValid => !Errors.Any();
        public ErrorRule[] Errors { get; }

        public bool HasErrorRules => Errors.Any();

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var error in Errors)
            {
                sb.AppendLine(error.ToString());
            }
            return sb.ToString();
        }

        public static ValidationResult With(params ErrorRule[] errors) => new ValidationResult(errors);
    }
}