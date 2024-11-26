using Application.Features.GetCustomers;
using Moq;
using Newtonsoft.Json;
using PactNet;
using PactNet.Output.Xunit;
using System.Net;
using Xunit.Abstractions;

namespace Contract.Tests
{
    public class ConsumerPactTest
    {
        private readonly IPactBuilderV3 pactBuilder;
        private readonly Mock<IHttpClientFactory> _httpClientLibraryMock;

        public ConsumerPactTest(ITestOutputHelper output)
        {
            var pact = Pact.V3("Client API", "Address API", new PactConfig
            {
                LogLevel = PactLogLevel.Debug,
                Outputters =
                [
                    new XunitOutput(output)
                ],
                PactDir = "../../pacts/"
            });

            pactBuilder = pact.WithHttpInteractions();
            _httpClientLibraryMock = new Mock<IHttpClientFactory>();
        }

        [Fact]
        public async Task Should_Return_Matched_Address()
        {
            // Arrange
            var exampleResponse = new CustomerAddress("street 1", "1", "city 1");

            // Create expectation(s) using the fluent API
            // first request, then response
            pactBuilder
                .UponReceiving("A GET request to get customer address")
                    .Given("There is available data")
                    .WithRequest(HttpMethod.Get, "/address/customer-address")
                    .WithQuery("customerId", "F4D610EC-36AF-407B-8F60-E0FBE6946D7B")
                .WillRespond()
                    .WithStatus(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithJsonBody(exampleResponse);

            // Act/Assert
            await pactBuilder.VerifyAsync(async ctx =>
            {
                // call the mock provider
                var providerClientFactory = new ProviderClientFactory(ctx.MockServerUri);
                var client = providerClientFactory.CreateClient();

                var response = await client.GetAsync("/address/customer-address?customerId=F4D610EC-36AF-407B-8F60-E0FBE6946D7B");

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var address = JsonConvert.DeserializeObject<CustomerAddress>(jsonResponse);

                Assert.NotNull(response);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            });
        }

        [Fact]
        public async Task Should_Return_Null_When_No_Matched_Address()
        {
            // Arrange

            // Create expectation(s) using the fluent API
            // first request, then response
            pactBuilder
                .UponReceiving("A valid GET request to /address/customer-address?customerId={customerId}")
                    .Given("There is available data")
                    .WithRequest(HttpMethod.Get, "/address/customer-address")
                    .WithQuery("customerId", "F4D610EC-36AF-407B-8F60-E0FBE6946D7B")
                .WillRespond()
                    .WithStatus(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithJsonBody("");

            // Act/Assert
            await pactBuilder.VerifyAsync(async ctx =>
            {
                // call the mock provider
                var providerClientFactory = new ProviderClientFactory(ctx.MockServerUri);
                var client = providerClientFactory.CreateClient();

                var response = await client.GetAsync("/address/customer-address?customerId=F4D610EC-36AF-407B-8F60-E0FBE6946D7B");

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var address = JsonConvert.DeserializeObject<CustomerAddress?>(jsonResponse);

                Assert.Null(address);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            });
        }
    }
}
