using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleIMS.Domain.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;

        // Foreign Key
        [Required]
        public int UserId { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; } = new List<SalesInvoice>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<PartRequest> PartRequests { get; set; } = new List<PartRequest>();
    }
}
