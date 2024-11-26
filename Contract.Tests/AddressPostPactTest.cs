using Moq;
using PactNet;
using PactNet.Output.Xunit;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit.Abstractions;

namespace Contract.Tests
{
    public class AddressPostPactTest
    {
        private readonly IPactBuilderV3 pactBuilder;
        private readonly Mock<IHttpClientFactory> _httpClientLibraryMock;

        public AddressPostPactTest(ITestOutputHelper output)
        {
            var pact = Pact.V3("Client API", "Address API", new PactConfig
            {
                LogLevel = PactLogLevel.Debug,
                Outputters =
                [
                    new XunitOutput(output)
                ],
                PactDir = "../../../pacts/"
            });

            pactBuilder = pact.WithHttpInteractions();
            _httpClientLibraryMock = new Mock<IHttpClientFactory>();
        }

        [Fact]
        public async Task Should_Return_200_Status_Code()
        {
            // Arrange
            var route = "/address/address-create";
            var expectedRequestBody = new
            {
                customerExternalId = "F4D610EC-36AF-407B-8F60-E0FBE6946D7B",
                streetName = "street 1",
                streetNumber = "1",
                city = "city 1",
                zipcode = "1234"
            };

            var jsonRequestBody = JsonSerializer.Serialize(expectedRequestBody);

            // Create expectation(s) using the fluent API
            // first request, then response
            pactBuilder
                .UponReceiving("A valid request to add new address")
                    .WithRequest(HttpMethod.Post, route)
                    .WithBody(jsonRequestBody, "application/json")
                .WillRespond()
                    .WithStatus(HttpStatusCode.OK);

            // Act/Assert
            await pactBuilder.VerifyAsync(async ctx =>
            {
                // call the mock provider
                var providerClientFactory = new ProviderClientFactory(ctx.MockServerUri);
                var client = providerClientFactory.CreateClient();

                var body = JsonContent.Create(expectedRequestBody);
                var response = await client.PostAsync(route, body);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                Assert.NotNull(response);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            });
        }


        [Fact]
        public async Task Should_Return_400_Status_Code_If_Invalid_Address_Provided()
        {
            // Arrange
            var route = "/address/address-create";
            var expectedRequestBody = new
            {
                customerExternalId = "F4D610EC-36AF-407B-8F60-E0FBE6946D7B",
                streetName = "street 1",
                streetNumber = "1"
            };

            var jsonRequestBody = JsonSerializer.Serialize(expectedRequestBody);

            // Create expectation(s) using the fluent API
            // first request, then response
            pactBuilder
                .UponReceiving("A valid request to add new address")
                    .WithRequest(HttpMethod.Post, route)
                    .WithBody(jsonRequestBody, "application/json")
                .WillRespond()
                    .WithStatus(HttpStatusCode.BadRequest);

            // Act/Assert
            await pactBuilder.VerifyAsync(async ctx =>
            {
                // call the mock provider
                var providerClientFactory = new ProviderClientFactory(ctx.MockServerUri);
                var client = providerClientFactory.CreateClient();

                var body = JsonContent.Create(expectedRequestBody);
                var response = await client.PostAsync(route, body);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                Assert.NotNull(response);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            });
        }
    }
}
