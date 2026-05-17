using Microsoft.AspNetCore.Mvc;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces.IServices;

namespace VehicleIMS.API.Controllers
{
    [ApiController]
    [Route("api/staff/[controller]")]
    public class SalesInvoiceController : ControllerBase
    {
        private readonly ISalesInvoiceService _service;

        public SalesInvoiceController(ISalesInvoiceService service)
        {
            _service = service;
        }

        // POST api/staff/salesinvoice
        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateSalesInvoiceDto dto)
        {
            try
            {
                var result = await _service.CreateInvoiceAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/staff/salesinvoice
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllInvoicesAsync();
            return Ok(result);
        }

        // GET api/staff/salesinvoice/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetInvoiceByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Invoice not found." });
            return Ok(result);
        }

        // GET api/staff/salesinvoice/customer/5
        [HttpGet("customer/{customerId:int}")]
        public async Task<IActionResult> GetByCustomer(int customerId)
        {
            var result = await _service.GetInvoicesByCustomerIdAsync(customerId);
            return Ok(result);
        }
    }
}