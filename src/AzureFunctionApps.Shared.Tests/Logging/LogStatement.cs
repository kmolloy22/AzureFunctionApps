using Microsoft.Extensions.Logging;

namespace AzureFunctionApps.Shared.Tests.Logging
{
    public class LogStatement
    {
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public override string ToString()
        {
            var exception = Exception?.Message == null
                ? string.Empty
                : $" :: {Exception.Message}";

            return $"{LogLevel} :: {Message}{exception}".TrimEnd();
        }
    }
}