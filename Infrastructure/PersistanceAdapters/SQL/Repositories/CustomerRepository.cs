using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customer.ToListAsync();
    }

    public async Task CreateCustomerAsync(Customer customer)
    {
        await _context.Customer.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(int customerId)
    {
        var customer = await _context.Customer.FindAsync(customerId);
        if (customer != null)
        {
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Customer.FindAsync(customerId);
    }


    public async Task<Customer?> FindCustomerByNameAndDobAsync(string forename, string surname, DateTime dateOfBirth)
    {
        return await _context.Customer
            .FirstOrDefaultAsync(c => c.Forename == forename && c.Surname == surname && c.DateOfBirth.Date == dateOfBirth.Date);
    }
}

