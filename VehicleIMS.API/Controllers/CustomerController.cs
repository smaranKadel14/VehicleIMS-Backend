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
}
