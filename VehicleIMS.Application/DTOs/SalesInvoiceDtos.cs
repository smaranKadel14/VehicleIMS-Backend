using System.ComponentModel.DataAnnotations;

namespace VehicleIMS.Application.DTOs
{
    public class CreateSalesInvoiceDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<SalesInvoiceItemDto> Items { get; set; } = new();
    }

    public class SalesInvoiceItemDto
    {
        [Required]
        public int PartId { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}