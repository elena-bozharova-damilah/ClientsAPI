using Application.Features.GetCustomers;
using Bogus;

namespace Unit.Tests.DataFaker
{
    public class GetCustomerResponseFaker : Faker<GetCustomerResponse>
    {
        public IEnumerable<GetCustomerResponse> WithRandomParameters(int numberOfCustomers)
        {
            CustomInstantiator(f => new GetCustomerResponse(f.Random.String2(5), f.Random.String2(10), f.Random.String(6), f.Date.Past()));

            return Generate(numberOfCustomers);
        }
    }
}
