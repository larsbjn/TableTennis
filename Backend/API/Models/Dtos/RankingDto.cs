namespace API.Models.Dtos;

/// <summary>
/// Data Transfer Object for Ranking
/// </summary>
public class RankingDto
{
    /// <summary>
    /// Gets or sets the name of the ranking.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the number of games played.
    /// </summary>
    public int GamesPlayed { get; set; }

    /// <summary>
    /// Gets or sets the number of wins.
    /// </summary>
    public int Wins { get; set; }

    /// <summary>
    /// Gets or sets the number of losses.
    /// </summary>
    public int Losses { get; set; }

    /// <summary>
    /// Gets or sets the win percentage.
    /// </summary>
    public double WinPercentage { get; set; }

    /// <summary>
    /// Gets or sets the Elo rating.
    /// </summary>
    public double Elo { get; set; }

    /// <summary>
    /// Gets or sets the TAA rating.
    /// </summary>
    public double TAA { get; set; }
}