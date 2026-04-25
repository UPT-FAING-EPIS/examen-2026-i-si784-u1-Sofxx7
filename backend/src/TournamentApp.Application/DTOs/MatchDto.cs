namespace TournamentApp.Application.DTOs;

public class MatchDto
{
    public Guid Id { get; set; }
    public Guid TournamentId { get; set; }
    public Guid? HomeTeamId { get; set; }
    public Guid? AwayTeamId { get; set; }
    public int Round { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class CreateMatchDto
{
    public Guid TournamentId { get; set; }
    public Guid? HomeTeamId { get; set; }
    public Guid? AwayTeamId { get; set; }
    public int Round { get; set; }
    public DateTime? ScheduledDate { get; set; }
}

public class UpdateMatchResultDto
{
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }
}
