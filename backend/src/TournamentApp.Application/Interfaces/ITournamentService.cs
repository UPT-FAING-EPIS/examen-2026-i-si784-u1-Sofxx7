using TournamentApp.Application.DTOs;

namespace TournamentApp.Application.Interfaces;

public interface ITournamentService
{
    Task<IEnumerable<TournamentDto>> GetAllAsync();
    Task<TournamentDto?> GetByIdAsync(Guid id);
    Task<TournamentDto> CreateAsync(CreateTournamentDto dto);
}
