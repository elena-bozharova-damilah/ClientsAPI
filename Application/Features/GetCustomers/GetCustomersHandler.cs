using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Features.GetCustomers;

public class GetCustomersHandler : IRequestHandler<GetAllCustomersRequest, IEnumerable<GetCustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<GetCustomerResponse>> Handle(
        GetAllCustomersRequest request,
        CancellationToken cancellationToken)
    {
        var customers = (await _customerRepository.GetAllCustomersAsync())
                  .Select(c => new GetCustomerResponse(c.Forename, c.Surname, c.Phone, c.DateOfJoining))
                  .ToList();


        return customers;
    }

}

