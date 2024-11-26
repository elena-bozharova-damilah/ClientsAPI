using MediatR;

namespace Application.Features.AddCustomerAddress
{
    public record AddCustomerAddressRequest(
        int CustomerId,
        string StreetName,
        string StreetNumber,
        string ZipCode,
        string City) : IRequest;
}
