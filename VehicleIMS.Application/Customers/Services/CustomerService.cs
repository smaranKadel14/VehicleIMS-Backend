using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.Customers.DTOs;
using VehicleIMS.Application.Customers.Interfaces;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Customers.Services;

public class CustomerService : ICustomerService
{
    private readonly IAppDbContext _context;

    public CustomerService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerResponse?> GetCustomerByIdAsync(
        int customerId,
        CancellationToken cancellationToken = default)
    {
        var customer = await _context.Customers
            .AsNoTracking()
            .Include(c => c.User)
            .Include(c => c.Vehicles)
            .FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);

        return customer is null ? null : MapCustomer(customer);
    }

    private static CustomerResponse MapCustomer(Customer customer)
    {
        return new CustomerResponse
        {
            Id = customer.Id,
            UserId = customer.UserId,
            Username = customer.User?.Username ?? string.Empty,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            Address = customer.Address,
            Vehicles = customer.Vehicles
                .Select(vehicle => new VehicleResponse
                {
                    Id = vehicle.Id,
                    Make = vehicle.Make,
                    Model = vehicle.Model,
                    Year = vehicle.Year,
                    VIN = vehicle.VIN,
                    LicensePlate = vehicle.LicensePlate
                })
                .ToList()
        };
    }
}
