using Accounting.Application.DTOs;
using Accounting.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _service;

    public SuppliersController(ISupplierService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll(CancellationToken cancellationToken)
    {
        var suppliers = await _service.GetAllAsync(cancellationToken);
        return Ok(suppliers);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SupplierDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var supplier = await _service.GetByIdAsync(id, cancellationToken);
        if (supplier is null)
        {
            return NotFound();
        }

        return Ok(supplier);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] SupplierDto dto, CancellationToken cancellationToken)
    {
        var id = await _service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SupplierDto dto, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(id, dto, cancellationToken);
        return NoContent();
    }
}
