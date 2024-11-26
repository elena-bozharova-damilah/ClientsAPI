using Application.Exceptions;
using Application.Interfaces.HttpClients.Address;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Features.AddCustomerAddress
{
    public class AddCustomerAddressHandler : IRequestHandler<AddCustomerAddressRequest>
    {
        private readonly IAddressHttpClient _addressHttpClient;
        private readonly ICustomerRepository _customerRepository;

        public AddCustomerAddressHandler(IAddressHttpClient addressHttpClient, ICustomerRepository customerRepository)
        {
            _addressHttpClient = addressHttpClient;
            _customerRepository = customerRepository;
        }

        public async Task Handle(AddCustomerAddressRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(request.CustomerId);

            if (customer is null)
            {
                throw new CustomerNotFoundException();
            }

            await _addressHttpClient.AddCustomerAddress(
                 customer.ExternalId,
                 request.StreetName,
                 request.StreetNumber,
                 request.ZipCode,
                 request.City);
        }
    }
}
