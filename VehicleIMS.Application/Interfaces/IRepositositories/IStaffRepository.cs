using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Interfaces
{
    public interface IStaffRepository
    {
        Task<Staff> RegisterAsync(User user, Staff staff);
        Task<Staff?> GetByIdAsync(int id);
        Task<Staff?> GetByEmailAsync(string email);
        Task<IEnumerable<Staff>> GetAllAsync();
        Task UpdateAsync(Staff staff);
        Task<bool> UsernameExistsAsync(string username);
    }
}