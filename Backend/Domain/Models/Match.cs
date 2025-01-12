namespace Domain.Models;

public class Match
{
    public int Id { get; set; }
    public required ICollection<Player> Players { get; set; }
    public bool IsFinished { get; set; }
    public NumberOfSets NumberOfSets { get; set; }
    public DateTime? Date { get; set; }
    public string? News { get; set; }
    public string? ExtraInfo1 { get; set; }
    public string? ExtraInfo2 { get; set; }
}