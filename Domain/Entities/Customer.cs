using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    public string Forename { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfJoining { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsDeleted { get; set; }

    public Guid ExternalId { get; set; }

    public Customer()
    {

    }

    private Customer(string forename, string surname, string email, string phone, DateTime dateOfJoining, DateTime dateOfBirth)
    {
        Forename = forename;
        Surname = surname;
        Email = email;
        Phone = phone;
        DateOfJoining = dateOfJoining;
        DateOfBirth = dateOfBirth;
        IsDeleted = false;
        ExternalId = Guid.NewGuid();
    }

    public static Customer Create(
        string forename,
        string surname,
        string email,
        string phone,
        DateTime dateOfJoining,
        DateTime dateOfBirth)
    {

        // TODO: if date of birth is less than 18 years, throw an exception
        if (dateOfBirth.AddYears(18) > DateTime.UtcNow)
        {
            throw new Exception("Customer must be at least 18 years old");
        }

        return new Customer(forename, surname, email, phone, dateOfJoining, dateOfBirth);
    }
}

