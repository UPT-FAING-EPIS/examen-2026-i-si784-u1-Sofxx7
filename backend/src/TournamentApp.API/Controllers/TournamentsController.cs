using Microsoft.AspNetCore.Mvc;
using TournamentApp.Application.DTOs;
using TournamentApp.Application.Interfaces;

namespace TournamentApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TournamentsController : ControllerBase
{
    private readonly ITournamentService _tournamentService;

    public TournamentsController(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tournaments = await _tournamentService.GetAllAsync();
        return Ok(tournaments);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tournament = await _tournamentService.GetByIdAsync(id);
        if (tournament == null) return NotFound();
        return Ok(tournament);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTournamentDto dto)
    {
        var created = await _tournamentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
