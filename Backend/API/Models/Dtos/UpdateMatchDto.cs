namespace API.Models.Dtos;

/// <summary>
/// Data transfer object for updating a match.
/// </summary>
public class UpdateMatchDto
{
    /// <summary>
    /// Gets or sets the ID of the match.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the winner.
    /// </summary>
    public int? WinnerId { get; set; }

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
    /// Gets or sets a value indicating whether to update the winner.
    /// </summary>
    public bool UpdateWinner { get; set; }
}