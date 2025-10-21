using Accounting.Application.DTOs;
using Accounting.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomersController(ICustomerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll(CancellationToken cancellationToken)
    {
        var customers = await _service.GetAllAsync(cancellationToken);
        return Ok(customers);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var customer = await _service.GetByIdAsync(id, cancellationToken);
        if (customer is null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CustomerDto dto, CancellationToken cancellationToken)
    {
        var id = await _service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CustomerDto dto, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(id, dto, cancellationToken);
        return NoContent();
    }
}
