using System.Threading;
using System.Threading.Tasks;
using VehicleIMS.Application.Customers.DTOs;

namespace VehicleIMS.Application.Customers.Interfaces;

public interface ICustomerService
{
    Task<CustomerHistoryResponse?> GetCustomerHistoryAsync(
        int customerId,
        CancellationToken cancellationToken = default);
}
