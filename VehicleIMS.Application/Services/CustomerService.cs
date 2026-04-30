using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.Customers.DTOs;
using VehicleIMS.Application.Customers.Interfaces;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IAppDbContext _context;

    public CustomerService(IAppDbContext context)
    {
        _context = context;
    }

    // ── helper: map Customer entity → CustomerResponse ────────────────────────
    private static CustomerResponse MapToResponse(Customer c) => new()
    {
        Id       = c.Id,
        UserId   = c.UserId,
        Username = c.User?.Username ?? string.Empty,
        FirstName = c.FirstName,
        LastName  = c.LastName,
        Email     = c.Email,
        Phone     = c.Phone,
        Address   = c.Address,
        Vehicles  = c.Vehicles.Select(v => new VehicleResponse
        {
            Id           = v.Id,
            Make         = v.Make,
            Model        = v.Model,
            Year         = v.Year,
            VIN          = v.VIN,
            LicensePlate = v.LicensePlate,
        }).ToList(),
    };

    // ── existing methods your lead already defined in the interface ───────────

    public async Task<CustomerResponse> RegisterCustomerWithVehicleAsync(
        CreateCustomerWithVehicleRequestDTO request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implemented by teammate.");
    }

    public async Task<CustomerResponse?> GetCustomerByIdAsync(
        int customerId,
        CancellationToken cancellationToken = default)
    {
        var customer = await _context.Customers
            .Include(c => c.User)
            .Include(c => c.Vehicles)
            .FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);

        return customer is null ? null : MapToResponse(customer);
    }

    public async Task<CustomerHistoryResponse?> GetCustomerHistoryAsync(
        int customerId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implemented by teammate.");
    }

    // ── Feature 10: Search customers ──────────────────────────────────────────

    public async Task<List<CustomerResponse>> SearchCustomersAsync(
        string? query,
        string searchType,
        string status,
        string sortBy,
        CancellationToken cancellationToken = default)
    {
        var all = await _context.Customers
            .Include(c => c.User)
            .Include(c => c.Vehicles)
            .ToListAsync(cancellationToken);

        // ── Text search ───────────────────────────────────────────────────────
        if (!string.IsNullOrWhiteSpace(query))
        {
            var q = query.ToLower().Trim();

            all = searchType switch
            {
                "Name" => all.Where(c =>
                    (c.FirstName + " " + c.LastName).ToLower().Contains(q)).ToList(),

                "Phone" => all.Where(c =>
                    c.Phone.Replace(" ", "").Replace("-", "")
                            .Contains(q.Replace(" ", "").Replace("-", ""))).ToList(),

                "Vehicle" => all.Where(c =>
                    c.Vehicles.Any(v =>
                        v.LicensePlate.ToLower().Contains(q) ||
                        v.VIN.ToLower().Contains(q))).ToList(),

                "ID" => all.Where(c => c.Id.ToString() == q).ToList(),

                _ => all.Where(c =>                                     // "All"
                    (c.FirstName + " " + c.LastName).ToLower().Contains(q) ||
                    c.Phone.Contains(q) ||
                    c.Vehicles.Any(v =>
                        v.LicensePlate.ToLower().Contains(q) ||
                        v.VIN.ToLower().Contains(q)) ||
                    c.Id.ToString() == q).ToList(),
            };
        }

        // ── Sort ──────────────────────────────────────────────────────────────
        all = sortBy switch
        {
            "Vehicles" => all.OrderByDescending(c => c.Vehicles.Count).ToList(),
            _          => all.OrderBy(c => c.FirstName).ToList(),       // "Name" default
        };

        return all.Select(MapToResponse).ToList();
    }
}