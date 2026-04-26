using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleIMS.Domain.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Scheduled";

        [MaxLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Foreign Keys
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int VehicleId { get; set; }

        // Navigation properties
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = null!;

        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
