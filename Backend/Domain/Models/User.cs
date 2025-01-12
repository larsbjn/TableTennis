namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Initials { get; set; }
    public double Elo { get; set; }
    public virtual ICollection<Player> Players { get; set; }
}