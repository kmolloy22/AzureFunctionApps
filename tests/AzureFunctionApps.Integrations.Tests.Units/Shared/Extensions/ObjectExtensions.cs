using AzureFunctionApps.Shared.Kernel.Serialization;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace AzureFunctionApps.Integrations.Tests.Units.Shared.Extensions
{
    internal static class ObjectExtensions
    {
        internal static HttpRequest ToHttpRequest(this object body)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            sw.Write(Serializer.ToJson(body));
            sw.Flush();
            ms.Position = 0;

            var request = Substitute.For<HttpRequest>();
            request.Body.Returns(ms);

            return request;
        }
    }

    internal static class HttpRequestFactory
    {
        public static HttpRequest Empty()
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            sw.Write(string.Empty);
            sw.Flush();
            ms.Position = 0;

            var request = Substitute.For<HttpRequest>();
            request.Body.Returns(ms);

            return request;
        }
    }
}