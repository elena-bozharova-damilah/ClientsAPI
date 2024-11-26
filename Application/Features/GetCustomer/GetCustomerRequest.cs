using Application.Features.GetCustomers;
using MediatR;

namespace Application.Features.GetCustomer;
public record GetCustomerRequest(int CustomerId) : IRequest<GetCustomerResponse>;

