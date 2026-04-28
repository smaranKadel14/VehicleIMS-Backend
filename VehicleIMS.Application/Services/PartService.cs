using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Customers.Interfaces;
using VehicleIMS.Application.Interfaces.IServices;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Services
{
    public class PartService : IPartService
    {
        private readonly IAppDbContext _context;

        public PartService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<PartResponse> CreatePartAsync(CreatePartRequest request, CancellationToken cancellationToken = default)
        {
            var part = new Part
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                SKU = request.SKU,
                VendorId = request.VendorId
            };

            _context.Parts.Add(part);
            await _context.SaveChangesAsync(cancellationToken);

            // Fetch vendor name for response
            var vendor = await _context.Vendors.FindAsync(new object[] { request.VendorId }, cancellationToken);

            return MapToResponse(part, vendor?.Name ?? "Unknown");
        }

        public async Task<PartResponse?> GetPartByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var part = await _context.Parts
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            return part != null ? MapToResponse(part, part.Vendor?.Name ?? "Unknown") : null;
        }

        public async Task<List<PartResponse>> GetAllPartsAsync(CancellationToken cancellationToken = default)
        {
            var parts = await _context.Parts
                .Include(p => p.Vendor)
                .ToListAsync(cancellationToken);

            return parts.ConvertAll(p => MapToResponse(p, p.Vendor?.Name ?? "Unknown"));
        }

        private static PartResponse MapToResponse(Part part, string vendorName)
        {
            return new PartResponse
            {
                Id = part.Id,
                Name = part.Name,
                Description = part.Description,
                Price = part.Price,
                StockQuantity = part.StockQuantity,
                SKU = part.SKU,
                VendorId = part.VendorId,
                VendorName = vendorName
            };
        }
    }
}
