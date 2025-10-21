using Accounting.Application.DTOs;
using Accounting.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class JournalEntriesController : ControllerBase
{
    private readonly IJournalService _journalService;

    public JournalEntriesController(IJournalService journalService)
    {
        _journalService = journalService;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] JournalEntryDto dto, CancellationToken cancellationToken)
    {
        var user = User.Identity?.Name ?? "system";
        var id = await _journalService.CreateAsync(dto, user, cancellationToken);
        return Ok(id);
    }

    [HttpPost("{id:int}/post")]
    public async Task<IActionResult> Post(int id, CancellationToken cancellationToken)
    {
        var user = User.Identity?.Name ?? "system";
        await _journalService.PostAsync(id, user, cancellationToken);
        return NoContent();
    }
}
