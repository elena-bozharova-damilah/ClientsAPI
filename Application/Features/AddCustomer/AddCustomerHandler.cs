using Application.Exceptions;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Features.AddCustomer;

public class AddCustomerHandler : IRequestHandler<AddCustomerRequest>
{
    private readonly ICustomerRepository _customerRepository;

    public AddCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(AddCustomerRequest request, CancellationToken cancellationToken)
    {
        var existingCustomer = await _customerRepository.FindCustomerByNameAndDobAsync(
            request.Forename,
            request.Surname,
            request.DateOfBirth);

        if (existingCustomer is not null)
        {
            throw new CustomerAlreadyExistsException();
        }

        var customer = Customer.Create(
            request.Forename,
            request.Surname,
            request.Email,
            request.Phone,
            DateTime.UtcNow,
            request.DateOfBirth);

        await _customerRepository.CreateCustomerAsync(customer);
    }
}

