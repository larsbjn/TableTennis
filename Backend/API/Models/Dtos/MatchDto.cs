using Domain.Models;

namespace API.Models.Dtos;

public class MatchDto
{
    public int Player1 { get; set; }
    public int Player2 { get; set; }
    public int Winner { get; set; }
    public DateTime Date { get; set; }
    public string? News { get; set; }
    public string? ExtraInfo1 { get; set; }
    public string? ExtraInfo2 { get; set; }

    public static implicit operator MatchDto(Match match) => new()
    {
        Date = match.Date,
        News = match.News,
        ExtraInfo1 = match.ExtraInfo1,
        ExtraInfo2 = match.ExtraInfo2
    };
}