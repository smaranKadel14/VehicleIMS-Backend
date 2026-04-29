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
            return StatusCode(StatusCodes.Status201Created, customer);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
