using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleIMS.Domain.Models
{
    public class SalesInvoiceItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        // Foreign Keys
        [Required]
        public int SalesInvoiceId { get; set; }

        [Required]
        public int PartId { get; set; }

        // Navigation properties
        [ForeignKey("SalesInvoiceId")]
        public virtual SalesInvoice SalesInvoice { get; set; } = null!;

        [ForeignKey("PartId")]
        public virtual Part Part { get; set; } = null!;
    }
}
