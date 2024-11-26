using MediatR;

namespace Application.Features.AddCustomer;

public record AddCustomerRequest(string Forename,
        string Surname,
        string Email,
        string Phone,
        DateTime DateOfBirth) : IRequest;

