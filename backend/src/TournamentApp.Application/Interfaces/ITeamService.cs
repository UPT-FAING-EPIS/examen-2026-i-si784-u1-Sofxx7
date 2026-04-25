using TournamentApp.Application.DTOs;

namespace TournamentApp.Application.Interfaces;

public interface ITeamService
{
    Task<IEnumerable<TeamDto>> GetByTournamentIdAsync(Guid tournamentId);
    Task<IEnumerable<TeamDto>> GetByUserIdAsync(Guid userId);
    Task<TeamDto?> GetByIdAsync(Guid id);
    Task<TeamDto> CreateAsync(CreateTeamDto dto);
}
