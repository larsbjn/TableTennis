using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace API.Services;

/// <summary>
/// Service for calculating Elo ratings.
/// </summary>
/// <param name="userRepository">The repository for user data.</param>
public class EloService(IUserRepository userRepository) : IEloService
{
    /// <summary>
    /// Calculates and persists the Elo rating for each player based on a match.
    /// </summary>
    /// <param name="match">The match for which to calculate the Elo rating.</param>
    /// <exception cref="ArgumentException">Thrown when a player is not found.</exception>
    public async Task AdjustEloForMatch(Match match)
    {
        var playerA = await userRepository.Get(match.Player1.Id);
        var playerB = await userRepository.Get(match.Player2.Id);
        if (playerA == null || playerB == null) throw new ArgumentException("Player not found");

        var rA = playerA.Elo;
        var rB = playerB.Elo;

        var eA = 1 / (1 + Math.Pow(10, (rB - rA) / 400));
        var eB = 1 / (1 + Math.Pow(10, (rA - rB) / 400));

        const int k = 20;
        var sA = match.Winner == playerA ? 1 : 0;
        var sB = match.Winner == playerB ? 1 : 0;

        playerA.Elo = rA + k * (sA - eA);
        playerB.Elo = rB + k * (sB - eB);

        await userRepository.Update(playerA);
        await userRepository.Update(playerB);
    }
}