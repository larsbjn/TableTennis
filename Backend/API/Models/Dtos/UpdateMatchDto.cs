namespace API.Models.Dtos;

/// <summary>
/// Data transfer object for updating a match.
/// </summary>
public class UpdateMatchDto
{
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
    
    /// <summary>
    /// Gets or sets the scores of the players.
    /// </summary>
    public required List<UpdateScoreDto> Scores { get; set; }
    
}

/// <summary>
/// Represents the score update for a player.
/// </summary>
public class UpdateScoreDto
{
    /// <summary>
    /// Gets or sets the ID of the player.
    /// </summary>
    public int PlayerId { get; set; }

    /// <summary>
    /// Gets or sets the score of the player.
    /// </summary>
    public int Score { get; set; }
}