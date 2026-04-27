using VehicleIMS.Application.DTOs;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Services
{
    public interface ICustomerService
    {
        Task<(bool Success, string Message, int? CustomerId)> RegisterAsync(RegisterCustomerDto dto);
        Task<string> UpdateProfileAsync(int customerId, UpdateProfileDto dto);
        Task<string> AddVehicleAsync(int customerId, VehicleDto dto);
        Task<string> UpdateVehicleAsync(int vehicleId, VehicleDto dto);
        Task<IEnumerable<Vehicle>> GetVehiclesAsync(int customerId);
    }
}
