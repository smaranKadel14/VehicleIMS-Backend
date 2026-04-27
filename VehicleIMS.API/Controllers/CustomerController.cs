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

    [HttpPost]
    public async Task<ActionResult<CustomerResponse>> RegisterCustomer(
        [FromBody] CreateCustomerWithVehicleRequestDTO request,
        CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _customerService.RegisterCustomerWithVehicleAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerResponse>> GetCustomerById(
        int id,
        CancellationToken cancellationToken)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id, cancellationToken);

        if (customer is null)
        {
            return NotFound(new { message = "Customer not found." });
        }

        return Ok(customer);
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
