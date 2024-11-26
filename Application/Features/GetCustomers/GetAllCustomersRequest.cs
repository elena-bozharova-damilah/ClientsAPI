using MediatR;

namespace Application.Features.GetCustomers;

public record GetAllCustomersRequest : IRequest<IEnumerable<GetCustomerResponse>>;
