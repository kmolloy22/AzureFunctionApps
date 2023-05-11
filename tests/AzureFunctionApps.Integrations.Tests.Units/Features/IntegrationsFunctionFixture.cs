using AzureFunctionApps.Integrations.Tests.Units.Shared;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace AzureFunctionApps.Integrations.Tests.Units.Features
{
    public class IntegrationsFunctionFixture
    {
        public ILogger Logger { get; }

        public FunctionFactory FunctionFactory { get; }

        public IntegrationsFunctionFixture()
        {
            Logger = Substitute.For<ILogger>();

            FunctionFactory = FunctionFactory.Setup
                .Mock(Logger);

            //Logger = Substitute.For<ILogger>();
        }

        public void ResetSubstitutes()
        {
            FunctionFactory.ResetSubstitutes();
        }
    }
}