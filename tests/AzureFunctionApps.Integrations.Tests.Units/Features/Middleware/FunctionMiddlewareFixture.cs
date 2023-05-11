using AzureFunctionApps.Integrations.Features.Middleware;

namespace AzureFunctionApps.Integrations.Tests.Units.Features.Middleware
{
    public class FunctionMiddlewareFixture : IntegrationsFunctionFixture
    {
        public FunctionMiddleware Function { get; }

        public FunctionMiddlewareFixture()
        {
            Function = FunctionFactory.CreateFunction<FunctionMiddleware>();
        }
    }
}