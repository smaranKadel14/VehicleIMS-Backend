using Microsoft.AspNetCore.Mvc;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces.IServices;

namespace VehicleIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartRequestController : ControllerBase
    {
        private readonly IPartRequestService _service;
        public PartRequestController(IPartRequestService service) { _service = service; }

        [HttpPost("{customerId}")]
        public async Task<IActionResult> SubmitPartRequest(int customerId, [FromBody] PartRequestDto dto)
        {
            var result = await _service.RequestAsync(customerId, dto);
            return Ok(new { message = result });
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetByCustomer(int customerId)
        {
            var result = await _service.GetByCustomerIdAsync(customerId);
            return Ok(result);
        }
    }
}