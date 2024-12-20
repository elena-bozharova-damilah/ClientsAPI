﻿using Application.Exceptions;
using Application.Features.GetCustomers;
using Application.Interfaces.HttpClients.Address;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Features.GetCustomer;

public class GetCustomerHandler : IRequestHandler<GetCustomerRequest, GetCustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressHttpClient _addressHttpClient;

    public GetCustomerHandler(ICustomerRepository customerRepository, IAddressHttpClient addressHttpClient)
    {
        _customerRepository = customerRepository;
        _addressHttpClient = addressHttpClient;
    }

    public async Task<GetCustomerResponse> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(request.CustomerId)
            ?? throw new CustomerNotFoundException();

        return new GetCustomerResponse(
            customer.Forename,
            customer.Surname,
            customer.Phone,
            customer.DateOfJoining);
    }
}

