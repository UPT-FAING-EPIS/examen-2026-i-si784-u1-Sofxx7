using TournamentApp.Domain.Enums;

namespace TournamentApp.Domain.Entities;

public class Tournament
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Sport { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxTeams { get; set; }
    public TournamentStatus Status { get; set; } = TournamentStatus.Draft;
    public Guid OrganizerId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User Organizer { get; set; } = null!;
    public ICollection<Team> Teams { get; set; } = new List<Team>();
    public ICollection<Match> Matches { get; set; } = new List<Match>();
}
