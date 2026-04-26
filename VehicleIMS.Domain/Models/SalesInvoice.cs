using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleIMS.Domain.Models
{
    public class SalesInvoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string InvoiceNumber { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        // Foreign Key
        [Required]
        public int CustomerId { get; set; }

        // Navigation properties
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = null!;

        public virtual ICollection<SalesInvoiceItem> SalesInvoiceItems { get; set; } = new List<SalesInvoiceItem>();
    }
}
