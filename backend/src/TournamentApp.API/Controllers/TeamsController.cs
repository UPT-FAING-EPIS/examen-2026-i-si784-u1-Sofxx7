using Microsoft.AspNetCore.Mvc;
using TournamentApp.Application.DTOs;
using TournamentApp.Application.Interfaces;

namespace TournamentApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByTournament([FromQuery] Guid tournamentId)
    {
        if (tournamentId == Guid.Empty)
            return BadRequest("tournamentId is required");

        var teams = await _teamService.GetByTournamentIdAsync(tournamentId);
        return Ok(teams);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var team = await _teamService.GetByIdAsync(id);
        if (team == null) return NotFound();
        return Ok(team);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTeamDto dto)
    {
        var created = await _teamService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
