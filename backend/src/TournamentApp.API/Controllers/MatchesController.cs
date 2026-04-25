using Microsoft.AspNetCore.Mvc;
using TournamentApp.Application.DTOs;
using TournamentApp.Application.Interfaces;

namespace TournamentApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchesController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByTournament([FromQuery] Guid tournamentId)
    {
        var matches = await _matchService.GetByTournamentIdAsync(tournamentId);
        return Ok(matches);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMatchDto dto)
    {
        var created = await _matchService.CreateAsync(dto);
        return Ok(created);
    }

    [HttpPut("{id:guid}/result")]
    public async Task<IActionResult> UpdateResult(Guid id, UpdateMatchResultDto dto)
    {
        var success = await _matchService.UpdateResultAsync(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }
}
