using VehicleIMS.Application.DTOs;

namespace VehicleIMS.Application.Interfaces.IServices
{
    public interface IStaffService
    {
        Task<StaffResponse> RegisterStaffAsync(RegisterStaffDto dto);
        Task<StaffResponse?> GetStaffByIdAsync(int staffId);
        Task<IEnumerable<StaffResponse>> GetAllStaffAsync();
        Task<string> UpdateStaffRoleAsync(int staffId, UpdateStaffRoleDto dto);
        Task<string> DeactivateStaffAsync(int staffId);
    }
}