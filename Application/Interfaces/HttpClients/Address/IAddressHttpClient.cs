namespace Application.Interfaces.HttpClients.Address;

public interface IAddressHttpClient
{
    Task<AddressResponse?> GetAddress(Guid customerExternalId);
    Task AddCustomerAddress(Guid customerExternalId, string StreetName, string StreetNumber, string ZipCode, string City);
}

