using Microsoft.AspNetCore.Mvc;
using VehicleIMS.Application.Customers.DTOs;
using VehicleIMS.Application.Customers.Interfaces;

namespace VehicleIMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{id:int}/history")]
    public async Task<ActionResult<CustomerHistoryResponse>> GetCustomerHistory(
        int id,
        CancellationToken cancellationToken)
    {
        var history = await _customerService.GetCustomerHistoryAsync(id, cancellationToken);

        if (history is null)
        {
            return NotFound(new { message = "Customer not found." });
        }

        return Ok(history);
    }
}
