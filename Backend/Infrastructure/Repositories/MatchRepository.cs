using Domain;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MatchRepository(DatabaseContext databaseContext) : IMatchRepository
{
    public async Task<IEnumerable<Match>> GetAll()
    {
        return await databaseContext.Matches
            .Include(m => m.Players)
            .ThenInclude(p => p.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Match>> GetBetweenDates(DateTime startDate, DateTime endDate)
    {
        return await databaseContext.Matches
            .Include(m => m.Players)
            .ThenInclude(p => p.User)
            .Where(m => m.Date >= startDate && m.Date <= endDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Match>> GetLatestWithNews(int count)
    {
        return await databaseContext.Matches
            .Where(m => !string.IsNullOrEmpty(m.News))
            .OrderByDescending(m => m.Date)
            .Take(count)
            .ToListAsync();
    }

    public async Task<Match?> Get(int id)
    {
        var match = await databaseContext.Matches
            .Include(m => m.Players)
            .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(x => x.Id == id);
        return match;
    }

    public async Task<Match> Update(Match match)
    {
        databaseContext.Matches.Update(match);
        await databaseContext.SaveChangesAsync();
        return match;
    }

    public async Task<int> Create(int player1Id, int player2Id, NumberOfSets numberOfSets)
    {
        var user1 = await databaseContext.Users.FindAsync(player1Id);
        var user2 = await databaseContext.Users.FindAsync(player2Id);
        if (user1 == null || user2 == null)
        {
            throw new ArgumentException("Player not found");
        }

        var player1 = (Player)user1;
        var player2 = (Player)user2;

        var match = new Match
        {
            Players = new List<Player> { player1, player2 },
            NumberOfSets = numberOfSets,
            Date = DateTime.Now
        };
        await databaseContext.Matches.AddAsync(match);
        await databaseContext.SaveChangesAsync();
        return match.Id;
    }

    public async Task Delete(int id)
    {
        var match = await databaseContext.Matches.FindAsync(id);
        if (match != null)
        {
            databaseContext.Matches.Remove(match);
            await databaseContext.SaveChangesAsync();
        }
    }
}