using TournamentApp.Domain.Enums;

namespace TournamentApp.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.Player;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Tournament> OrganizedTournaments { get; set; } = new List<Tournament>();
    public ICollection<Player> PlayerProfiles { get; set; } = new List<Player>();
}
