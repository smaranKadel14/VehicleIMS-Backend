using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleIMS.Domain.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;

        // Foreign Key
        [Required]
        public int UserId { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    }
}
