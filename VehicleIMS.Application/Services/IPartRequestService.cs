using VehicleIMS.Application.DTOs;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Services
{
    public interface IPartRequestService
    {
        Task<string> RequestAsync(int customerId, PartRequestDto dto);
        Task<IEnumerable<PartRequest>> GetByCustomerIdAsync(int customerId);
    }
}