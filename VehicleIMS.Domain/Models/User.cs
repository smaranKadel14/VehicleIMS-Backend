using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleIMS.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "User";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
