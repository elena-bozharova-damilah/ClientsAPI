using Application;
using Application.Interfaces.HttpClients.Address;
using System.Net.Http.Json;

namespace Infrastructure.Http.Clients.Address;

public class AddressHttpClient : IAddressHttpClient
{
    private readonly IHttpClientFactory _factory;
    private const string GetCustomerAddressUrl = "/address/customer-address/";
    private const string CreateCustomerAddressUrl = "/address/address-create";

    public AddressHttpClient(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<AddressResponse?> GetAddress(Guid customerExternalId)
    {
        using var client = _factory.CreateClient();

        client.BaseAddress = new Uri($"{Constants.AddressAPIUrl}");

        var response
            = await client.GetFromJsonAsync<AddressResponse>($"{GetCustomerAddressUrl}?customerId={customerExternalId}");

        return response;
    }

    public async Task AddCustomerAddress(
        Guid customerExternalId,
        string StreetName,
        string StreetNumber,
        string ZipCode,
        string City)
    {
        using var client = _factory.CreateClient();

        client.BaseAddress = new Uri($"{Constants.AddressAPIUrl}");

        try
        {
            var response = await client.PostAsJsonAsync($"{CreateCustomerAddressUrl}", new
            {
                customerExternalId,
                streetName = StreetName,
                streetNumber = StreetNumber,
                city = City,
                zipcode = ZipCode
            });
        }
        catch (Exception e)
        {
            throw;
        }

    }
}

