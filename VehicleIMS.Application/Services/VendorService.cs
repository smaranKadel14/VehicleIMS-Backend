using Microsoft.EntityFrameworkCore;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Customers.Interfaces;
using VehicleIMS.Application.Interfaces.IServices;
using VehicleIMS.Domain.Models;

namespace VehicleIMS.Application.Services
{
    public class VendorService : IVendorService
    {
        private readonly IAppDbContext _context;

        public VendorService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<VendorResponse> CreateVendorAsync(CreateVendorRequest request, CancellationToken cancellationToken = default)
        {
            var vendor = new Vendor
            {
                Name = request.Name,
                ContactPerson = request.ContactPerson,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address
            };

            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync(cancellationToken);

            return MapToResponse(vendor);
        }

        public async Task<VendorResponse?> GetVendorByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var vendor = await _context.Vendors.FindAsync(new object[] { id }, cancellationToken);
            return vendor != null ? MapToResponse(vendor) : null;
        }

        public async Task<List<VendorResponse>> GetAllVendorsAsync(CancellationToken cancellationToken = default)
        {
            var vendors = await _context.Vendors.ToListAsync(cancellationToken);
            return vendors.ConvertAll(MapToResponse);
        }

        private static VendorResponse MapToResponse(Vendor vendor)
        {
            return new VendorResponse
            {
                Id = vendor.Id,
                Name = vendor.Name,
                ContactPerson = vendor.ContactPerson,
                Email = vendor.Email,
                Phone = vendor.Phone,
                Address = vendor.Address
            };
        }
    }
}
