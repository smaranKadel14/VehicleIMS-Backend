using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Interfaces
{
    public interface ISalesInvoiceRepository
    {
        Task<SalesInvoice> CreateAsync(SalesInvoice invoice, List<SalesInvoiceItem> items);
        Task<SalesInvoice?> GetByIdAsync(int id);
        Task<IEnumerable<SalesInvoice>> GetAllAsync();
        Task<IEnumerable<SalesInvoice>> GetByCustomerIdAsync(int customerId);
        Task<Part?> GetPartByIdAsync(int partId);
        Task UpdatePartStockAsync(Part part);
    }
}