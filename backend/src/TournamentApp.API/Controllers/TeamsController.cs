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
    public async Task<IActionResult> Get([FromQuery] Guid? tournamentId, [FromQuery] Guid? userId)
    {
        IEnumerable<TeamDto> teams = new List<TeamDto>();
        
        if (tournamentId.HasValue && tournamentId != Guid.Empty)
            teams = await _teamService.GetByTournamentIdAsync(tournamentId.Value);
        else if (userId.HasValue && userId != Guid.Empty)
            teams = await _teamService.GetByUserIdAsync(userId.Value);
        else
            return BadRequest("tournamentId or userId is required");

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
