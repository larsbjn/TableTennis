using Domain.Models;

namespace API.Models.Dtos;

/// <summary>
/// Data transfer object for a match.
/// </summary>
public class MatchDto
{
    /// <summary>
    /// Gets or sets the ID of the match.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first player of the match.
    /// </summary>
    public required UserDto Player1 { get; set; }

    /// <summary>
    /// Gets or sets the second player of the match.
    /// </summary>
    public required UserDto Player2 { get; set; }

    /// <summary>
    /// Gets or sets the winner of the match.
    /// </summary>
    public UserDto? Winner { get; set; }

    /// <summary>
    /// Gets or sets the date of the match.
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Gets or sets the news related to the match.
    /// </summary>
    public string? News { get; set; }

    /// <summary>
    /// Gets or sets the extra information 1 of the match.
    /// </summary>
    public string? ExtraInfo1 { get; set; }

    /// <summary>
    /// Gets or sets the extra information 2 of the match.
    /// </summary>
    public string? ExtraInfo2 { get; set; }

    /// <summary>
    /// Implicitly converts a <see cref="Match"/> object to a <see cref="MatchDto"/> object.
    /// </summary>
    /// <param name="match">The match object to convert.</param>
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