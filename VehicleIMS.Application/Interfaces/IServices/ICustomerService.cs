using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using VehicleIMS.Application.Customers.DTOs;

namespace VehicleIMS.Application.Customers.Interfaces;

public interface ICustomerService
{
    Task<CustomerResponse> RegisterCustomerWithVehicleAsync(
        CreateCustomerWithVehicleRequestDTO request,
        CancellationToken cancellationToken = default);

    Task<CustomerResponse?> GetCustomerByIdAsync(
        int customerId,
        CancellationToken cancellationToken = default);

    Task<CustomerHistoryResponse?> GetCustomerHistoryAsync(
        int customerId,
        CancellationToken cancellationToken = default);

    // ← ADD THIS (your Feature 10)
    Task<List<CustomerResponse>> SearchCustomersAsync(
        string? query,
        string searchType,
        string status,
        string sortBy,
        CancellationToken cancellationToken = default);
}