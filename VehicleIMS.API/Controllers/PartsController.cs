using Microsoft.AspNetCore.Mvc;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces.IServices;

namespace VehicleIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartsController : ControllerBase
    {
        private readonly IPartService _partService;

        public PartsController(IPartService partService)
        {
            _partService = partService;
        }

        [HttpPost]
        public async Task<ActionResult<PartResponse>> CreatePart([FromBody] CreatePartRequest request, CancellationToken cancellationToken)
        {
            var part = await _partService.CreatePartAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetPartById), new { id = part.Id }, part);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PartResponse>> GetPartById(int id, CancellationToken cancellationToken)
        {
            var part = await _partService.GetPartByIdAsync(id, cancellationToken);
            if (part == null) return NotFound();
            return Ok(part);
        }

        [HttpGet]
        public async Task<ActionResult<List<PartResponse>>> GetAllParts(CancellationToken cancellationToken)
        {
            var parts = await _partService.GetAllPartsAsync(cancellationToken);
            return Ok(parts);
        }
    }
}
