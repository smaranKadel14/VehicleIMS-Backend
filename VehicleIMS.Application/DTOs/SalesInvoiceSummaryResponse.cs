using System;

namespace VehicleIMS.Application.Customers.DTOs;

public class SalesInvoiceSummaryResponse
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal FinalTotal { get; set; }
    public decimal TotalAmount { get; set; } // Keep for backward compatibility
}
