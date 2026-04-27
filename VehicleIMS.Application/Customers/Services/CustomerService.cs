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

    public async Task<CustomerResponse> RegisterCustomerWithVehicleAsync(
        CreateCustomerWithVehicleRequestDTO request,
        CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim();
        var username = request.Username.Trim();
        var phone = request.Phone.Trim();
        var vin = request.VIN.Trim().ToUpperInvariant();
        var licensePlate = request.LicensePlate.Trim().ToUpperInvariant();

        if (await _context.Users.AnyAsync(u => u.Email == email || u.Username == username, cancellationToken))
        {
            throw new InvalidOperationException("A user with the same email or username already exists.");
        }

        if (await _context.Customers.AnyAsync(c => c.Phone == phone || c.Email == email, cancellationToken))
        {
            throw new InvalidOperationException("A customer with the same phone or email already exists.");
        }

        if (await _context.Vehicles.AnyAsync(v => v.VIN == vin || v.LicensePlate == licensePlate, cancellationToken))
        {
            throw new InvalidOperationException("A vehicle with the same VIN or license plate already exists.");
        }

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = request.PasswordHash.Trim(),
            Role = "Customer"
        };

        var customer = new Customer
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = email,
            Phone = phone,
            Address = request.Address.Trim(),
            User = user
        };

        var vehicle = new Vehicle
        {
            Make = request.Make.Trim(),
            Model = request.Model.Trim(),
            Year = request.Year,
            VIN = vin,
            LicensePlate = licensePlate,
            Customer = customer
        };

        customer.Vehicles.Add(vehicle);

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return MapCustomer(customer);
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

    public async Task<CustomerHistoryResponse?> GetCustomerHistoryAsync(
        int customerId,
        CancellationToken cancellationToken = default)
    {
        var customer = await _context.Customers
            .AsNoTracking()
            .Include(c => c.User)
            .Include(c => c.Vehicles)
            .Include(c => c.SalesInvoices)
            .Include(c => c.Appointments)
                .ThenInclude(appointment => appointment.Vehicle)
            .FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);

        if (customer is null)
        {
            return null;
        }

        var salesInvoices = customer.SalesInvoices
            .OrderByDescending(invoice => invoice.Date)
            .Select(invoice => new SalesInvoiceSummaryResponse
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                Date = invoice.Date,
                TotalAmount = invoice.TotalAmount
            })
            .ToList();

        var serviceHistory = customer.Appointments
            .OrderByDescending(appointment => appointment.AppointmentDate)
            .Select(appointment => new ServiceHistoryResponse
            {
                Id = appointment.Id,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                Notes = appointment.Notes,
                VehicleId = appointment.VehicleId,
                VehicleName = $"{appointment.Vehicle.Make} {appointment.Vehicle.Model}".Trim(),
                LicensePlate = appointment.Vehicle.LicensePlate
            })
            .ToList();

        return new CustomerHistoryResponse
        {
            Customer = MapCustomer(customer),
            SalesInvoices = salesInvoices,
            ServiceHistory = serviceHistory,
            TotalInvoices = salesInvoices.Count,
            TotalSpent = salesInvoices.Sum(invoice => invoice.TotalAmount),
            TotalServices = serviceHistory.Count
        };
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
