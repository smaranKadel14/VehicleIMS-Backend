using System.Threading;
using System.Threading.Tasks;
using VehicleIMS.Application.Customers.DTOs;

namespace VehicleIMS.Application.Customers.Interfaces;

public interface ICustomerService
{
    Task<CustomerResponse> RegisterCustomerWithVehicleAsync(
        CreateCustomerWithVehicleRequestDTO request,
        CancellationToken cancellationToken = default);
}
