using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleIMS.Domain.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Make { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        [Required]
        [MaxLength(17)]
        public string VIN { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string LicensePlate { get; set; } = string.Empty;

        // Foreign Key
        [Required]
        public int CustomerId { get; set; }

        // Navigation properties
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
