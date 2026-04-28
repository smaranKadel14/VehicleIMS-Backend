using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleIMS.Application.DTOs;

namespace VehicleIMS.Application.Interfaces.IServices
{
    public interface IVendorService
    {
        Task<VendorResponse> CreateVendorAsync(CreateVendorRequest request, CancellationToken cancellationToken = default);
        Task<VendorResponse?> GetVendorByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<VendorResponse>> GetAllVendorsAsync(CancellationToken cancellationToken = default);
    }
}
