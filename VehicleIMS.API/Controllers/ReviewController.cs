using Microsoft.AspNetCore.Mvc;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces.IServices;

namespace VehicleIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;
        public ReviewController(IReviewService service) { _service = service; }

        [HttpPost("{customerId}")]
        public async Task<IActionResult> Submit(int customerId, [FromBody] ReviewDto dto)
        {
            var result = await _service.SubmitAsync(customerId, dto);
            if (result.Contains("already"))
                return BadRequest(new { message = result });
            return Ok(new { message = result });
        }
    }
}