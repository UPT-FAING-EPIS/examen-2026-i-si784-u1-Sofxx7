using TournamentApp.Application.DTOs;
using TournamentApp.Application.Interfaces;
using TournamentApp.Domain.Entities;
using TournamentApp.Domain.Interfaces;

namespace TournamentApp.Application.Services;

public class TeamService : ITeamService
{
    private readonly IGenericRepository<Team> _repository;

    public TeamService(IGenericRepository<Team> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TeamDto>> GetByTournamentIdAsync(Guid tournamentId)
    {
        var teams = await _repository.FindAsync(t => t.TournamentId == tournamentId);
        return teams.Select(t => new TeamDto
        {
            Id = t.Id,
            Name = t.Name,
            TournamentId = t.TournamentId,
            CaptainId = t.CaptainId
        });
    }

    public async Task<IEnumerable<TeamDto>> GetByUserIdAsync(Guid userId)
    {
        var teams = await _repository.FindAsync(t => t.CaptainId == userId || t.Players.Any(p => p.UserId == userId));
        return teams.Select(t => new TeamDto
        {
            Id = t.Id,
            Name = t.Name,
            TournamentId = t.TournamentId,
            CaptainId = t.CaptainId
        });
    }

    public async Task<TeamDto?> GetByIdAsync(Guid id)
    {
        var t = await _repository.GetByIdAsync(id);
        if (t == null) return null;

        return new TeamDto
        {
            Id = t.Id,
            Name = t.Name,
            TournamentId = t.TournamentId,
            CaptainId = t.CaptainId
        };
    }

    public async Task<TeamDto> CreateAsync(CreateTeamDto dto)
    {
        var team = new Team
        {
            Name = dto.Name,
            TournamentId = dto.TournamentId,
            CaptainId = dto.CaptainId
        };

        await _repository.AddAsync(team);
        await _repository.SaveChangesAsync();

        return new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            TournamentId = team.TournamentId,
            CaptainId = team.CaptainId
        };
    }
}
