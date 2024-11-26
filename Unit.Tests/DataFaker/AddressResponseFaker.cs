using Application.Interfaces.HttpClients.Address;
using Bogus;

namespace Unit.Tests.DataFaker
{
    public class AddressResponseFaker : Faker<AddressResponse>
    {
        public AddressResponse WithRandomParameters()
        {
            CustomInstantiator(f => new AddressResponse
            {
                StreetName = f.Address.StreetName(),
                StreetNumber = f.Address.StreetSuffix(),
                City = f.Address.City()
            });

            return Generate();
        }
    }
}
