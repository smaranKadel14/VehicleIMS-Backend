using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleIMS.Application.DTOs;

namespace VehicleIMS.Application.Interfaces.IServices
{
    public interface IPartService
    {
        Task<PartResponse> CreatePartAsync(CreatePartRequest request, CancellationToken cancellationToken = default);
        Task<PartResponse?> GetPartByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<PartResponse>> GetAllPartsAsync(CancellationToken cancellationToken = default);
    }
}
