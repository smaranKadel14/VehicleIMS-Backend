using Microsoft.AspNetCore.Mvc;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Services;

namespace VehicleIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCustomerDto dto)
        {
            var result = await _service.RegisterAsync(dto);

            if (!result.Success)
                return Conflict(new { message = result.Message });

            return CreatedAtAction(
                nameof(GetVehicles),
                new { customerId = result.CustomerId },
                new
                {
                    message = result.Message,
                    customerId = result.CustomerId
                });
        }

        [HttpPut("{customerId}/profile")]
        public async Task<IActionResult> UpdateProfile(int customerId, [FromBody] UpdateProfileDto dto)
        {
            var result = await _service.UpdateProfileAsync(customerId, dto);
            if (result.Contains("not found"))
                return NotFound(new { message = result });

            return Ok(new { message = result });
        }

        [HttpGet("{customerId}/vehicles")]
        public async Task<IActionResult> GetVehicles(int customerId)
        {
            var vehicles = await _service.GetVehiclesAsync(customerId);
            return Ok(vehicles);
        }

        [HttpPost("{customerId}/vehicles")]
        public async Task<IActionResult> AddVehicle(int customerId, [FromBody] VehicleDto dto)
        {
            var result = await _service.AddVehicleAsync(customerId, dto);
            if (result.Contains("not found"))
                return NotFound(new { message = result });

            return Ok(new { message = result });
        }

        [HttpPut("vehicles/{vehicleId}")]
        public async Task<IActionResult> UpdateVehicle(int vehicleId, [FromBody] VehicleDto dto)
        {
            var result = await _service.UpdateVehicleAsync(vehicleId, dto);
            if (result.Contains("not found"))
                return NotFound(new { message = result });

            return Ok(new { message = result });
        }
    }
}
