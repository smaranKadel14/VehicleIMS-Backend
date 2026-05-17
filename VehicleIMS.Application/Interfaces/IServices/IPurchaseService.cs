using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleIMS.Application.DTOs;

namespace VehicleIMS.Application.Interfaces.IServices
{
    public interface IPurchaseService
    {
        Task<PurchaseInvoiceResponse> CreatePurchaseInvoiceAsync(CreatePurchaseInvoiceRequest request, CancellationToken cancellationToken = default);
        Task<PurchaseInvoiceResponse?> GetPurchaseInvoiceByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<PurchaseInvoiceResponse>> GetAllPurchaseInvoicesAsync(CancellationToken cancellationToken = default);
    }
}
