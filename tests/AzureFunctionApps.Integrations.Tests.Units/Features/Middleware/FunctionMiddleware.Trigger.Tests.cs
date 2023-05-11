using AzureFunctionApps.Contracts.ValidationModels;
using AzureFunctionApps.Integrations.Features.Middleware;
using AzureFunctionApps.Integrations.Tests.Units.Shared.Extensions;
using AzureFunctionApps.Shared.Tests;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace AzureFunctionApps.Integrations.Tests.Units.Features.Middleware
{
    public class FunctionMiddlewareTests : IClassFixture<FunctionMiddlewareFixture>
    {
        private readonly FunctionMiddlewareFixture _fixture;

        private ILogger Logger => _fixture.Logger;

        private FunctionMiddleware Function => _fixture.Function;

        public FunctionMiddlewareTests(FunctionMiddlewareFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetSubstitutes();
        }

        [Fact]
        public async Task Run_ThrowsException_ReturnsBadRequestResponse()
        {
            //var ex = await Assert.ThrowsAsync<HttpRequestException>(async () => await Function.Run(null, Logger));

            //_ = RandomInstance.Single<ValidationRequest>();

            var response = await Function.Run(null, Logger);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Run_SuccessfulExecution_ReturnsOkResponse()
        {
            var request = RandomInstance.Single<ValidationRequest>();

            var response = await Function.Run(request.ToHttpRequest(), Logger);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().BeEquivalentTo("Middleware Response");
        }
    }
}