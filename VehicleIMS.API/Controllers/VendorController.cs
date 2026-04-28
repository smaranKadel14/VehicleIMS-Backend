using Microsoft.AspNetCore.Mvc;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces.IServices;

namespace VehicleIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpPost]
        public async Task<ActionResult<VendorResponse>> CreateVendor([FromBody] CreateVendorRequest request, CancellationToken cancellationToken)
        {
            var vendor = await _vendorService.CreateVendorAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetVendorById), new { id = vendor.Id }, vendor);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VendorResponse>> GetVendorById(int id, CancellationToken cancellationToken)
        {
            var vendor = await _vendorService.GetVendorByIdAsync(id, cancellationToken);
            if (vendor == null) return NotFound();
            return Ok(vendor);
        }

        [HttpGet]
        public async Task<ActionResult<List<VendorResponse>>> GetAllVendors(CancellationToken cancellationToken)
        {
            var vendors = await _vendorService.GetAllVendorsAsync(cancellationToken);
            return Ok(vendors);
        }
    }
}
