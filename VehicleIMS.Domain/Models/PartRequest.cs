using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleIMS.Domain.Models
{
    public class PartRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string PartName { get; set; } = string.Empty;

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        // Foreign Keys
        [Required]
        public int CustomerId { get; set; }

        public int? PartId { get; set; } // Nullable FK as requested

        // Navigation properties
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = null!;

        [ForeignKey("PartId")]
        public virtual Part? Part { get; set; }
    }
}
