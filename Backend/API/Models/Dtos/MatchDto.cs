using Domain.Models;

namespace API.Models.Dtos;

public class MatchDto
{
    public int Id { get; set; }
    public required UserDto Player1 { get; set; }
    public required UserDto Player2 { get; set; }
    public UserDto? Winner { get; set; }
    public DateTime? Date { get; set; }
    public string? News { get; set; }
    public string? ExtraInfo1 { get; set; }
    public string? ExtraInfo2 { get; set; }

    public static implicit operator MatchDto(Match match)
    {
        var dto = new MatchDto
        {
            Id = match.Id,
            Player1 = match.Player1,
            Player2 = match.Player2,
            Date = match.Date,
            News = match.News,
            ExtraInfo1 = match.ExtraInfo1,
            ExtraInfo2 = match.ExtraInfo2
        };
        if (match.Winner != null)
        {
            dto.Winner = match.Winner;
        }
        return dto;
    }
}