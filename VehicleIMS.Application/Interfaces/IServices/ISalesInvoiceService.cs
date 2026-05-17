using VehicleIMS.Application.DTOs;

namespace VehicleIMS.Application.Interfaces.IServices
{
    public interface ISalesInvoiceService
    {
        Task<SalesInvoiceResponse> CreateInvoiceAsync(CreateSalesInvoiceDto dto);
        Task<SalesInvoiceResponse?> GetInvoiceByIdAsync(int id);
        Task<IEnumerable<SalesInvoiceResponse>> GetAllInvoicesAsync();
        Task<IEnumerable<SalesInvoiceResponse>> GetInvoicesByCustomerIdAsync(int customerId);
    }
}