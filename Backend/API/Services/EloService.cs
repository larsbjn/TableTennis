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
        var winnerId = match.Players.First(p => p.IsWinner).User.Id;
        var playerIds = match.Players.Select(p => p.User.Id).ToArray();
        var playerA = await userRepository.Get(playerIds[0]);
        var playerB = await userRepository.Get(playerIds[1]);
        if (playerA == null || playerB == null) throw new ArgumentException("Player not found");
        
        var rA = playerA.Elo;
        var rB = playerB.Elo;
        
        var eA = 1 / (1 + Math.Pow(10, (rB - rA) / 400));
        var eB = 1 / (1 + Math.Pow(10, (rA - rB) / 400));
        
        const int k = 20;
        var sA = winnerId == playerA.Id ? 1 : 0;
        var sB = winnerId == playerB.Id ? 1 : 0;
        
        playerA.Elo = rA + k * (sA - eA);
        playerB.Elo = rB + k * (sB - eB);
        
        await userRepository.Update(playerA);
        await userRepository.Update(playerB);
    }
}