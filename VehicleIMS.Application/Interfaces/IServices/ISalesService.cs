using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleIMS.Application.DTOs;

namespace VehicleIMS.Application.Interfaces.IServices
{
    public interface ISalesService
    {
        Task<SalesInvoiceResponse> CreateSalesInvoiceAsync(CreateSalesInvoiceRequest request, CancellationToken cancellationToken = default);
        Task<SalesInvoiceResponse?> GetSalesInvoiceByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<SalesInvoiceResponse>> GetAllSalesInvoicesAsync(CancellationToken cancellationToken = default);
        Task<SendInvoiceEmailResponse> SendInvoiceEmailAsync(int id, SendInvoiceEmailRequest request, CancellationToken cancellationToken = default);
    }
}
