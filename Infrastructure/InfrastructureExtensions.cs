using Application.Interfaces;
using Application.Interfaces.HttpClients.Address;
using Domain.Interfaces.Repositories;
using Infrastructure.Http.Clients.Address;
using Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();

        serviceCollection.AddScoped<IAddressHttpClient, AddressHttpClient>();

        // register services

        serviceCollection.AddScoped<INumberService, NumberService>();
        serviceCollection.AddScoped<ICalculatorService, CalculatorService>();

        return serviceCollection;
    }
}

