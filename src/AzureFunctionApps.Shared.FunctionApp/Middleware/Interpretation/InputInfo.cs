namespace AzureFunctionApps.Shared.FunctionApp.Middleware.Interpretation
{
    public class InputInfo
    {
        public string Title { get; }
        public Dictionary<string, object> Logs { get; }
        public string[] Tags { get; set; }

        public InputInfo(string title, Dictionary<string, object> logs, string[] tags)
        {
            Title = title;
            Logs = logs;
            Tags = tags;
        }
    }
}