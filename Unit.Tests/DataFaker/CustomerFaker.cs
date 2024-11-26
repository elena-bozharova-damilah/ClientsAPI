using Bogus;
using Domain.Entities;

namespace Unit.Tests.DataFaker
{
    public class CustomerFaker : Faker<Customer>
    {
        public Customer WithRandomParameters()
        {
            CustomInstantiator(f => Customer.Create(f.Random.String(), f.Random.String(), f.Random.String(), f.Random.String2(6), f.Date.Past(), f.Date.Past()));

            return Generate();
        }
    }
}
