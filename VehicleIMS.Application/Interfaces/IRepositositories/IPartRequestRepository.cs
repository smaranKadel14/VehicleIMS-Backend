using VehicleIMS.Domain.Models;
namespace VehicleIMS.Application.Interfaces;

public interface IPartRequestRepository
{
    Task<PartRequest> AddAsync(PartRequest request);
    Task<IEnumerable<PartRequest>> GetByCustomerIdAsync(int customerId);
}

