namespace TournamentApp.Application.DTOs;

public class TeamDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid TournamentId { get; set; }
    public Guid? CaptainId { get; set; }
}

public class CreateTeamDto
{
    public string Name { get; set; } = string.Empty;
    public Guid TournamentId { get; set; }
    public Guid? CaptainId { get; set; }
}
