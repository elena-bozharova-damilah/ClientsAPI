namespace Application.Features.GetCustomers;

public record GetCustomerResponse(string Forename, string Surname, string PhoneNumber, DateTime DateOfJoining)
{
    public CustomerAddress? Address { get; set; } = null;
}

public record CustomerAddress(string StreetName, string StreetNumber, string City);

