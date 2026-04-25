namespace TournamentApp.Domain.Entities;

public class Team
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public Guid TournamentId { get; set; }
    public Guid? CaptainId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Tournament Tournament { get; set; } = null!;
    public User? Captain { get; set; }
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
    public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
}
