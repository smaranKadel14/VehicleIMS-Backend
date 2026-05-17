using System;

namespace VehicleIMS.Application.Customers.DTOs;

public class SalesInvoiceSummaryResponse
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
}
