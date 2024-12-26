using API.Hubs;
using API.Models.Dtos;
using Domain.Interfaces.Hubs;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers;

[Route("[controller]/[action]")]
public class RankingController(
    ILogger<RankingController> logger,
    IMatchRepository matchRepository,
    IUserRepository userRepository,
    IHubContext<RankingHub, IRankingHub> rankingHub)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RankingDto[]>> GetAll()
    {
        var rankings = new Dictionary<string, RankingDto>();

        // Add all users
        var users = await userRepository.GetAll();
        foreach (var user in users)
        {
            rankings[user.Name] = new RankingDto
            {
                Name = user.Name,
                GamesPlayed = 0,
                Wins = 0,
                Losses = 0
            };
        }

        // Add all matches played, wins and losses
        var matches = await matchRepository.GetAll();
        foreach (var match in matches.Where(m => m.Winner != null))
        {
            rankings[match.Player1.Name].GamesPlayed++;
            rankings[match.Player2.Name].GamesPlayed++;
            rankings[match.Winner!.Name].Wins++;
            var loser = match.Player1 == match.Winner ? match.Player2 : match.Player1;
            rankings[loser.Name].Losses++;
        }

        // Calculate win percentage
        foreach (var ranking in rankings.Values)
        {
            var winPercentage = ranking.Wins == 0 ? 0 : (double)ranking.Wins / ranking.GamesPlayed * 100;
            ranking.WinPercentage = Math.Round(winPercentage, 2);
        }

        // Calculate Elo
        foreach (var ranking in rankings.Values)
        {
            ranking.Elo = 1500 + 20 * (ranking.Wins - ranking.Losses);
        }
        
        var response = rankings.Values.ToArray().OrderByDescending(r => r.Elo);
        return Ok(response);
    }
}