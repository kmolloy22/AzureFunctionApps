using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionApps.Integrations.Tests.Units.Shared
{
    public class TestStartup : Startup
    {
        private readonly Action<IServiceCollection> _registerMocks;

        public TestStartup(Action<IServiceCollection> registerMocks)
        {
            _registerMocks = registerMocks;
        }

        protected override void RegisterAdditionalServices(IServiceCollection services)
        {
            base.RegisterAdditionalServices(services);

            _registerMocks(services);
        }
    }
}