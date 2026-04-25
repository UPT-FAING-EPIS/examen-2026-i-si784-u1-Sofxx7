namespace TournamentApp.Domain.Entities;

public class Result
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid MatchId { get; set; }
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }
    public Guid? WinnerId { get; set; }

    public Match Match { get; set; } = null!;
    public Team? Winner { get; set; }
}
