using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using VehicleIMS.Application.DTOs;
using VehicleIMS.Application.Interfaces.IServices;

namespace VehicleIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseInvoice([FromBody] CreatePurchaseInvoiceRequest request, CancellationToken cancellationToken)
        {
            var result = await _purchaseService.CreatePurchaseInvoiceAsync(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseInvoice(int id, CancellationToken cancellationToken)
        {
            var result = await _purchaseService.GetPurchaseInvoiceByIdAsync(id, cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchaseInvoices(CancellationToken cancellationToken)
        {
            var result = await _purchaseService.GetAllPurchaseInvoicesAsync(cancellationToken);
            return Ok(result);
        }
    }
}
