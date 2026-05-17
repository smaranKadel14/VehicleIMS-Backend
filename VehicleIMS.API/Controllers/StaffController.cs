using Microsoft.AspNetCore.Mvc;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces.IServices;

namespace VehicleIMS.API.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _service;

        public StaffController(IStaffService service)
        {
            _service = service;
        }

        // POST api/admin/staff/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterStaff([FromBody] RegisterStaffDto dto)
        {
            try
            {
                var result = await _service.RegisterStaffAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/admin/staff
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllStaffAsync();
            return Ok(result);
        }

        // GET api/admin/staff/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetStaffByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Staff member not found." });
            return Ok(result);
        }

        // PUT api/admin/staff/5/role
        [HttpPut("{id:int}/role")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateStaffRoleDto dto)
        {
            var result = await _service.UpdateStaffRoleAsync(id, dto);
            if (result.Contains("not found"))
                return NotFound(new { message = result });
            return Ok(new { message = result });
        }

        // DELETE api/admin/staff/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _service.DeactivateStaffAsync(id);
            if (result.Contains("not found"))
                return NotFound(new { message = result });
            return Ok(new { message = result });
        }
    }
}