namespace API.Models.Dtos;

public class RankingDto
{
    public string Name { get; set; }
    public int GamesPlayed { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public double WinPercentage { get; set; }
    public double Elo { get; set; }
    public double TAA { get; set; }
}