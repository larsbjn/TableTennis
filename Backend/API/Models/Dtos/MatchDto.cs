using Domain;
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
    public required PlayerDto[] Players { get; set; } = [];

    /// <summary>
    /// Gets or sets the date of the match.
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// The number of sets in the match.
    /// </summary>
    public NumberOfSets NumberOfSets { get; set; }
    
    /// <summary>
    /// Determines whether the match is finished.
    /// </summary>
    public bool IsFinished { get; set; }
    
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
            Players = match.Players.Select(player => (PlayerDto)player).ToArray(),
            NumberOfSets = match.NumberOfSets,
            Date = match.Date,
            News = match.News,
            ExtraInfo1 = match.ExtraInfo1,
            ExtraInfo2 = match.ExtraInfo2,
            IsFinished = match.IsFinished
        };

        return dto;
    }
}