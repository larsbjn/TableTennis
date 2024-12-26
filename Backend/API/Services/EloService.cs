using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace API.Services;

public class EloService(IUserRepository userRepository) : IEloService
{
    public async Task CalculateElo(Match match)
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