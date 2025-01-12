using Domain.Models;

namespace API.Models.Dtos;

/// <summary>
/// Data transfer object for a player.
/// </summary>
public class PlayerDto
{
    /// <summary>
    /// Gets or sets the user associated with the player.
    /// </summary>
    public required UserDto User { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the player is the winner.
    /// </summary>
    public bool IsWinner { get; set; }

    /// <summary>
    /// Gets or sets the score of the player.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Gets or sets the Elo rating of the player.
    /// </summary>
    public double Elo { get; set; }

    /// <summary>
    /// Implicitly converts a <see cref="Player"/> object to a <see cref="PlayerDto"/> object.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public static implicit operator PlayerDto(Player player)
    {
        return new PlayerDto
        {
            User = player.User,
            IsWinner = player.IsWinner,
            Elo = player.Elo,
            Score = player.Score
        };
    }
    
}