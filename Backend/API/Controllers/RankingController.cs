using API.Hubs;
using API.Models.Dtos;
using Domain.Extensions;
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
        var usersResponse = await userRepository.GetAll();
        var users = usersResponse as User[] ?? usersResponse.ToArray();
        foreach (var user in users)
        {
            rankings[user.Name] = new RankingDto
            {
                Name = user.Name,
                GamesPlayed = 0,
                Wins = 0,
                Losses = 0,
                Elo = Math.Round(user.Elo, 0)
            };
        }

        // Add all matches played, wins and losses
        var matchesResponse = await matchRepository.GetAll();
        var matches = matchesResponse as Match[] ?? matchesResponse.ToArray();
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

        // Calculate TAA
        var startOfWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
        var endOfWeek = startOfWeek.AddDays(7).AddTicks(-1);

        var matchesThisWeek = matches.Where(m =>
            m.Date >= startOfWeek && m.Date <= endOfWeek && m.Winner != null).ToArray();
        foreach (var user in users)
        {
            var won = matchesThisWeek.Count(m => m.Winner!.Id == user.Id);
            var played = matchesThisWeek.Count(m => m.Player1.Id == user.Id || m.Player2.Id == user.Id);
            
            var ratio = played == 0 ? 0 : (double)won / played * 100;

            rankings[user.Name].TAA = Math.Round(ratio, 2);
        }

        var response = rankings.Values.ToArray().OrderByDescending(r => r.Elo);
        return Ok(response);
    }
}