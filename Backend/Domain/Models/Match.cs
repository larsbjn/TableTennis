namespace Domain.Models;

public class Match
{
    public int Id { get; set; }
    public required User Player1 { get; set; }
    public required User Player2 { get; set; }
    public required User Winner { get; set; }
    public DateTime Date { get; set; }
    public string? News { get; set; }
    public string? ExtraInfo1 { get; set; }
    public string? ExtraInfo2 { get; set; }
}