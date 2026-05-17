using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces.IServices;

namespace VehicleIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalesInvoice([FromBody] CreateSalesInvoiceRequest request, CancellationToken cancellationToken)
        {
            var result = await _salesService.CreateSalesInvoiceAsync(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesInvoice(int id, CancellationToken cancellationToken)
        {
            var result = await _salesService.GetSalesInvoiceByIdAsync(id, cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalesInvoices(CancellationToken cancellationToken)
        {
            var result = await _salesService.GetAllSalesInvoicesAsync(cancellationToken);
            return Ok(result);
        }
    }
}
