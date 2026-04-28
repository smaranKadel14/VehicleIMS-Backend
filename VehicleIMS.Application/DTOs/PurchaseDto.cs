using System;
using System.Collections.Generic;

namespace VehicleIMS.Application.DTOs
{
    public class CreatePurchaseInvoiceRequest
    {
        public int VendorId { get; set; }
        public List<CreatePurchaseInvoiceItemRequest> Items { get; set; } = new();
    }

    public class CreatePurchaseInvoiceItemRequest
    {
        public int PartId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class PurchaseInvoiceResponse
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalTotal { get; set; }
        public List<PurchaseInvoiceItemResponse> Items { get; set; } = new();
    }

    public class PurchaseInvoiceItemResponse
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
