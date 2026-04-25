namespace TournamentApp.Domain.Entities;

public class Player
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TeamId { get; set; }
    public Guid? UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public Team Team { get; set; } = null!;
    public User? User { get; set; }
}
