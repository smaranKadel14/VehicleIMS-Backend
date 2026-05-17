using System.ComponentModel.DataAnnotations;

namespace VehicleIMS.Application.DTOs
{
    public class RegisterStaffDto
    {
        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Department { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Position { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Role { get; set; } = "Staff";
    }

    public class UpdateStaffRoleDto
    {
        [Required, MaxLength(20)]
        public string Role { get; set; } = string.Empty;
    }

    public class StaffResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}