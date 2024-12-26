namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Initials { get; set; }
    public double Elo { get; set; }
}