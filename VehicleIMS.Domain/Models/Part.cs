using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleIMS.Domain.Models
{
    public class Part
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        [Required]
        [MaxLength(50)]
        public string SKU { get; set; } = string.Empty;

        // Foreign Key
        [Required]
        public int VendorId { get; set; }

        // Navigation properties
        [ForeignKey("VendorId")]
        public virtual Vendor Vendor { get; set; } = null!;

        public virtual ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; } = new List<PurchaseInvoiceItem>();
        public virtual ICollection<SalesInvoiceItem> SalesInvoiceItems { get; set; } = new List<SalesInvoiceItem>();
        public virtual ICollection<PartRequest> PartRequests { get; set; } = new List<PartRequest>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
