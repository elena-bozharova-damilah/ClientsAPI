using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllCustomersAsync();
    Task CreateCustomerAsync(Customer customer);

    Task DeleteCustomerAsync(int customerId);
    Task<Customer?> GetCustomerByIdAsync(int customerId);
    Task<Customer?> FindCustomerByNameAndDobAsync(string forename, string surname, DateTime dateOfBirth);

}

