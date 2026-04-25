using TournamentApp.Application.DTOs;
using TournamentApp.Application.Interfaces;
using TournamentApp.Domain.Entities;
using TournamentApp.Domain.Enums;
using TournamentApp.Domain.Interfaces;

namespace TournamentApp.Application.Services;

public class MatchService : IMatchService
{
    private readonly IGenericRepository<Match> _matchRepository;
    private readonly IGenericRepository<Result> _resultRepository;

    public MatchService(IGenericRepository<Match> matchRepository, IGenericRepository<Result> resultRepository)
    {
        _matchRepository = matchRepository;
        _resultRepository = resultRepository;
    }

    public async Task<IEnumerable<MatchDto>> GetByTournamentIdAsync(Guid tournamentId)
    {
        var matches = await _matchRepository.FindAsync(m => m.TournamentId == tournamentId);
        return matches.Select(m => new MatchDto
        {
            Id = m.Id,
            TournamentId = m.TournamentId,
            HomeTeamId = m.HomeTeamId,
            AwayTeamId = m.AwayTeamId,
            Round = m.Round,
            ScheduledDate = m.ScheduledDate,
            Status = m.Status.ToString()
        });
    }

    public async Task<MatchDto> CreateAsync(CreateMatchDto dto)
    {
        var match = new Match
        {
            TournamentId = dto.TournamentId,
            HomeTeamId = dto.HomeTeamId,
            AwayTeamId = dto.AwayTeamId,
            Round = dto.Round,
            ScheduledDate = dto.ScheduledDate,
            Status = MatchStatus.Scheduled
        };

        await _matchRepository.AddAsync(match);
        await _matchRepository.SaveChangesAsync();

        return new MatchDto
        {
            Id = match.Id,
            TournamentId = match.TournamentId,
            HomeTeamId = match.HomeTeamId,
            AwayTeamId = match.AwayTeamId,
            Round = match.Round,
            ScheduledDate = match.ScheduledDate,
            Status = match.Status.ToString()
        };
    }

    public async Task<bool> UpdateResultAsync(Guid matchId, UpdateMatchResultDto dto)
    {
        var match = await _matchRepository.GetByIdAsync(matchId);
        if (match == null) return false;

        Guid? winnerId = null;
        if (dto.HomeScore > dto.AwayScore) winnerId = match.HomeTeamId;
        else if (dto.AwayScore > dto.HomeScore) winnerId = match.AwayTeamId;

        var result = new Result
        {
            MatchId = matchId,
            HomeScore = dto.HomeScore,
            AwayScore = dto.AwayScore,
            WinnerId = winnerId
        };

        await _resultRepository.AddAsync(result);
        
        match.Status = MatchStatus.Completed;
        _matchRepository.Update(match);

        await _resultRepository.SaveChangesAsync();
        await _matchRepository.SaveChangesAsync();

        return true;
    }
}
