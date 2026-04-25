using TournamentApp.Domain.Enums;

namespace TournamentApp.Application.DTOs;

public class TournamentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Sport { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxTeams { get; set; }
    public string Status { get; set; } = string.Empty;
    public Guid OrganizerId { get; set; }
}

public class CreateTournamentDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Sport { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxTeams { get; set; }
    public Guid OrganizerId { get; set; }
}
