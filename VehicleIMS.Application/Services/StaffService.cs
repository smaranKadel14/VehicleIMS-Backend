using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Application.Interfaces.IServices;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _repo;

        public StaffService(IStaffRepository repo)
        {
            _repo = repo;
        }

        public async Task<StaffResponse> RegisterStaffAsync(RegisterStaffDto dto)
        {
            var usernameExists = await _repo.UsernameExistsAsync(dto.Username);
            if (usernameExists)
                throw new InvalidOperationException("Username is already taken.");

            var emailExists = await _repo.GetByEmailAsync(dto.Email);
            if (emailExists != null)
                throw new InvalidOperationException("Email is already registered.");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow
            };

            var staff = new Staff
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Department = dto.Department,
                Position = dto.Position,
                IsActive = true,
                JoinedAt = DateTime.UtcNow
            };

            var created = await _repo.RegisterAsync(user, staff);
            return MapToResponse(created);
        }

        public async Task<StaffResponse?> GetStaffByIdAsync(int staffId)
        {
            var staff = await _repo.GetByIdAsync(staffId);
            if (staff == null) return null;
            return MapToResponse(staff);
        }

        public async Task<IEnumerable<StaffResponse>> GetAllStaffAsync()
        {
            var staffList = await _repo.GetAllAsync();
            return staffList.Select(MapToResponse);
        }

        public async Task<string> UpdateStaffRoleAsync(int staffId, UpdateStaffRoleDto dto)
        {
            var staff = await _repo.GetByIdAsync(staffId);
            if (staff == null)
                return "Staff member not found.";

            staff.User.Role = dto.Role;
            await _repo.UpdateAsync(staff);
            return $"Role updated to '{dto.Role}' successfully.";
        }

        public async Task<string> DeactivateStaffAsync(int staffId)
        {
            var staff = await _repo.GetByIdAsync(staffId);
            if (staff == null)
                return "Staff member not found.";

            staff.IsActive = false;
            await _repo.UpdateAsync(staff);
            return "Staff member deactivated successfully.";
        }

        private static StaffResponse MapToResponse(Staff staff) => new StaffResponse
        {
            Id = staff.Id,
            UserId = staff.UserId,
            Username = staff.User?.Username ?? string.Empty,
            FirstName = staff.FirstName,
            LastName = staff.LastName,
            Email = staff.Email,
            Phone = staff.Phone,
            Department = staff.Department,
            Position = staff.Position,
            Role = staff.User?.Role ?? string.Empty,
            IsActive = staff.IsActive,
            JoinedAt = staff.JoinedAt
        };
    }
}