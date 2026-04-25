using TournamentApp.Application.DTOs;
using TournamentApp.Application.Interfaces;
using TournamentApp.Domain.Entities;
using TournamentApp.Domain.Interfaces;

namespace TournamentApp.Application.Services;

public class TournamentService : ITournamentService
{
    private readonly IGenericRepository<Tournament> _repository;

    public TournamentService(IGenericRepository<Tournament> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TournamentDto>> GetAllAsync()
    {
        var tournaments = await _repository.GetAllAsync();
        return tournaments.Select(t => new TournamentDto
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description,
            Sport = t.Sport,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            MaxTeams = t.MaxTeams,
            Status = t.Status.ToString(),
            OrganizerId = t.OrganizerId
        });
    }

    public async Task<TournamentDto?> GetByIdAsync(Guid id)
    {
        var t = await _repository.GetByIdAsync(id);
        if (t == null) return null;

        return new TournamentDto
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description,
            Sport = t.Sport,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            MaxTeams = t.MaxTeams,
            Status = t.Status.ToString(),
            OrganizerId = t.OrganizerId
        };
    }

    public async Task<TournamentDto> CreateAsync(CreateTournamentDto dto)
    {
        var tournament = new Tournament
        {
            Name = dto.Name,
            Description = dto.Description,
            Sport = dto.Sport,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            MaxTeams = dto.MaxTeams,
            OrganizerId = dto.OrganizerId
        };

        await _repository.AddAsync(tournament);
        await _repository.SaveChangesAsync();

        return new TournamentDto
        {
            Id = tournament.Id,
            Name = tournament.Name,
            Description = tournament.Description,
            Sport = tournament.Sport,
            StartDate = tournament.StartDate,
            EndDate = tournament.EndDate,
            MaxTeams = tournament.MaxTeams,
            Status = tournament.Status.ToString(),
            OrganizerId = tournament.OrganizerId
        };
    }
}
