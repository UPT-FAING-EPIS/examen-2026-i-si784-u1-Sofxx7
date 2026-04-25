using TournamentApp.Application.DTOs;

namespace TournamentApp.Application.Interfaces;

public interface IMatchService
{
    Task<IEnumerable<MatchDto>> GetByTournamentIdAsync(Guid tournamentId);
    Task<MatchDto> CreateAsync(CreateMatchDto dto);
    Task<bool> UpdateResultAsync(Guid matchId, UpdateMatchResultDto dto);
}
