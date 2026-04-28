using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.Interfaces;
using VehicleIMS.Domain.Models;
using VehicleIMS.Infrastructure.Persistence;

namespace VehicleIMS.Infrastructure.Repositories
{
    public class PartRequestRepository : IPartRequestRepository
    {
        private readonly AppDbContext _context;
        public PartRequestRepository(AppDbContext context) { _context = context; }

        public async Task<PartRequest> AddAsync(PartRequest request)
        {
            _context.PartRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<IEnumerable<PartRequest>> GetByCustomerIdAsync(int customerId) =>
            await _context.PartRequests
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
    }
}