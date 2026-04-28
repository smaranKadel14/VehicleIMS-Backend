using Microsoft.AspNetCore.Mvc;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Services;

namespace VehicleIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;
        public AppointmentController(IAppointmentService service) { _service = service; }

        [HttpPost("{customerId}")]
        public async Task<IActionResult> Book(int customerId, [FromBody] AppointmentDto dto)
        {
            var result = await _service.BookAsync(customerId, dto);
            if (result.Contains("future"))
                return BadRequest(new { message = result });
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