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
