using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleIMS.Domain.Models
{
    public class PurchaseInvoiceItem
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
        public int PurchaseInvoiceId { get; set; }

        [Required]
        public int PartId { get; set; }

        // Navigation properties
        [ForeignKey("PurchaseInvoiceId")]
        public virtual PurchaseInvoice PurchaseInvoice { get; set; } = null!;

        [ForeignKey("PartId")]
        public virtual Part Part { get; set; } = null!;
    }
}
