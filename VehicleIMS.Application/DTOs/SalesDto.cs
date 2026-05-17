using System;
using System.Collections.Generic;

namespace VehicleIMS.Application.DTOs
{
    public class CreateSalesInvoiceRequest
    {
        public int CustomerId { get; set; }
        public List<CreateSalesInvoiceItemRequest> Items { get; set; } = new();
    }

    public class CreateSalesInvoiceItemRequest
    {
        public int PartId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class SalesInvoiceResponse
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalTotal { get; set; }
        public List<SalesInvoiceItemResponse> Items { get; set; } = new();
    }

    public class SalesInvoiceItemResponse
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
