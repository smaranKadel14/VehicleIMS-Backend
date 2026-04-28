using System.Collections.Generic;

namespace VehicleIMS.Application.Customers.DTOs;

public class CustomerHistoryResponse
{
    public CustomerResponse Customer { get; set; } = new();
    public List<SalesInvoiceSummaryResponse> SalesInvoices { get; set; } = new List<SalesInvoiceSummaryResponse>();
    public List<ServiceHistoryResponse> ServiceHistory { get; set; } = new List<ServiceHistoryResponse>();
    public decimal TotalSpent { get; set; }
    public int TotalInvoices { get; set; }
    public int TotalServices { get; set; }
}
