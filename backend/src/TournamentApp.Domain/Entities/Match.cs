using TournamentApp.Domain.Enums;

namespace TournamentApp.Domain.Entities;

public class Match
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TournamentId { get; set; }
    public Guid? HomeTeamId { get; set; }
    public Guid? AwayTeamId { get; set; }
    public int Round { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public MatchStatus Status { get; set; } = MatchStatus.Scheduled;
    public Guid? NextMatchId { get; set; } // Para avanzar en el bracket

    public Tournament Tournament { get; set; } = null!;
    public Team? HomeTeam { get; set; }
    public Team? AwayTeam { get; set; }
    public Match? NextMatch { get; set; }
    public Result? Result { get; set; }
}
