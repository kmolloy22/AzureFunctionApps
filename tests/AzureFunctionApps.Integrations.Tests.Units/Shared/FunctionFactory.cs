using AzureFunctionApps.Shared.Tests.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ClearExtensions;
using NSubstitute.Core;

namespace AzureFunctionApps.Integrations.Tests.Units.Shared
{
    public class FunctionFactory
    {
        public static FunctionFactory Setup => new FunctionFactory();

        private readonly Dictionary<Type, object> _injectedMocks = new Dictionary<Type, object>();
        private ITestLogger _logger;

        public FunctionFactory Mock<T>(T mock)
            where T : class
        {
            _injectedMocks.Add(typeof(T), mock);
            return this;
        }

        public FunctionFactory Mock<T>()
            where T : class
        {
            _injectedMocks.Add(typeof(T), Substitute.For<T>());
            return this;
        }

        public FunctionFactory Mock<TInterface, TType>(TType mock)
            where TType : class, TInterface
        {
            _injectedMocks.Add(typeof(TInterface), mock);
            return this;
        }

        public T GetMock<T>()
            where T : class
        {
            if (_injectedMocks.ContainsKey(typeof(T)))
                return (T)_injectedMocks[typeof(T)];

            return null;
        }

        public FunctionFactory AddLogger(ITestLogger logger)
        {
            _logger = logger;
            return this;
        }

        private IHost _host = null;

        private IHost BuildHost()
        {
            var startup = new TestStartup(RegisterServices);
            var builder = new HostBuilder()
                .ConfigureWebJobs(startup.Configure)
                .ConfigureAppConfiguration(AppCongiguration);

            builder.ConfigureLogging(logging =>
            {
                if (_logger != null)
                {
                    logging.AddProvider(new TestLoggerProvider(_logger));
                }
            });

            return builder.Build();
        }

        public TFunction CreateFunction<TFunction>()
        {
            _host ??= BuildHost();

            var type = typeof(TFunction);
            var arguments = type
                .GetConstructors()
                .First()
                .GetParameters()
                .Select(p =>
                {
                    var parameter = _host.Services.GetService(p.ParameterType);
                    if (parameter == null)
                    {
                        throw new ArgumentNullException($"Not available to resolve {typeof(TFunction).Name} -> {p.ParameterType}");
                    }

                    return parameter;
                })
                .ToArray();

            return (TFunction)Activator.CreateInstance(type, arguments);
        }

        private void AppCongiguration(HostBuilderContext hostBuilderContext, IConfigurationBuilder configurationBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(configuration);
        }

        private void RegisterServices(IServiceCollection collection)
        {
            OverrideRegisteredServicesWithMocks(collection, _injectedMocks);
        }

        // replace actual dependencies with mock implementations to isolate the behavior of the tested component.
        private static void OverrideRegisteredServicesWithMocks(IServiceCollection collection, Dictionary<Type, object> injectedMocks)
        {
            foreach (var injectedMock in injectedMocks)
            {
                var descriptor = collection.FirstOrDefault(p => p.ServiceType == injectedMock.Key);

                if (descriptor != null)
                    collection.Remove(descriptor);

                collection.AddSingleton(injectedMock.Key, injectedMock.Value);
            }
        }

        public void ResetSubstitutes()
        {
            foreach(var injectedMock in _injectedMocks.Values)
            {
                if (injectedMock is ICallRouterProvider provider)
                    provider.ClearSubstitute();
            }
        }
    }
}