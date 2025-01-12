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
        var rankings = new Dictionary<int, RankingDto>();

        // Add all users
        foreach (var user in users)
        {
            var played = user.Players.Count;
            var wins = user.Players.Count(p => p.IsWinner);
            var losses = played - wins;
            rankings[user.Id] = new RankingDto
            {
                Name = user.Name,
                GamesPlayed = played,
                Wins = wins,
                Losses = losses,
                Elo = Math.Round(user.Elo, 0)
            };
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
            m.Date >= startOfWeek && m.Date <= endOfWeek && m.IsFinished).ToArray();
        foreach (var user in users)
        {
            var won = matchesThisWeek.Count(m => m.Players.Any(p => p.User.Id == user.Id && p.IsWinner));
            var played = matchesThisWeek.Count(m => m.Players.Any(p => p.User.Id == user.Id));
            
            var ratio = played == 0 ? 0 : (double)won / played * 100;
        
            rankings[user.Id].TAA = Math.Round(ratio, 2);
        }

        return rankings.Values.OrderByDescending(r => r.Elo).ToList();
    }
}