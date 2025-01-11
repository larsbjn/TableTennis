using API.Models.Dtos;
using Domain.Extensions;
using Domain.Models;

namespace API.Handlers;

/// <summary>
/// Handler for managing rankings.
/// </summary>
public static class RankingHandler
{
    /// <summary>
    /// Gets the rankings based on the provided matches and users.
    /// </summary>
    /// <param name="matches">The list of matches.</param>
    /// <param name="users">The list of users.</param>
    /// <returns>A list of ranking data transfer objects.</returns>
    public static List<RankingDto> GetRankings(List<Match> matches, List<User> users)
    {
        
        var rankings = new Dictionary<string, RankingDto>();

        // Add all users
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

        return rankings.Values.OrderByDescending(r => r.Elo).ToList();
    }
}