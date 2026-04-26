using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleIMS.Domain.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        [Required]
        public int CustomerId { get; set; }

        public int? PartId { get; set; }

        // Navigation properties
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = null!;

        [ForeignKey("PartId")]
        public virtual Part? Part { get; set; }
    }
}
