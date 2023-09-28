namespace AzureFunctionApps.Shared.Kernel.Errors.Rules
{
    public class ErrorRule
    {
        public ErrorRule()
        { }

        public ErrorRule(string type, string title)
        {
            Type = type;
            Title = title;
        }

        public string Type { get; set; }
        public string Title { get; set; }
        public string Property { get; set; }
        public object AttemptedValue { get; set; }
        public string Details { get; set; }

        public override string ToString()
        {
            return $"{Type} - {Title}";
        }
    }
}