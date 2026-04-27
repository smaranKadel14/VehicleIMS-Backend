using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Services
{
    public class PartRequestService : IPartRequestService
    {
        private readonly IPartRequestRepository _repo;
        public PartRequestService(IPartRequestRepository repo) { _repo = repo; }

        public async Task<string> RequestAsync(int customerId, PartRequestDto dto)
        {
            var request = new PartRequest
            {
                CustomerId = customerId,
                PartName = dto.PartName,
                PartId = dto.PartId,
                RequestDate = DateTime.UtcNow,
                Status = "Pending"
            };
            await _repo.AddAsync(request);
            return "Part request submitted successfully.";
        }

        public async Task<IEnumerable<PartRequest>> GetByCustomerIdAsync(int customerId)
        {
            return await _repo.GetByCustomerIdAsync(customerId);
        }
    }
}