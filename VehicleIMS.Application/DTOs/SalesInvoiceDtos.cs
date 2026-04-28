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

    public class SalesInvoiceResponse
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public List<SalesInvoiceItemResponse> Items { get; set; } = new();
    }

    public class SalesInvoiceItemResponse
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal => Quantity * UnitPrice;
    }
}