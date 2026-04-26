using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleIMS.Domain.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ContactPerson { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<Part> Parts { get; set; } = new List<Part>();
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
    }
}
