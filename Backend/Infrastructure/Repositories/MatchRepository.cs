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
            .Include(m => m.Player1)
            .Include(m => m.Player2)
            .Include(m => m.Winner)
            .ToListAsync();
    }

    public async Task<IEnumerable<Match>> GetLatestWithNews(int count)
    {
        return await databaseContext.Matches
            .Where(m => !string.IsNullOrEmpty(m.News))
            .Include(m => m.Player1)
            .Include(m => m.Player2)
            .Include(m => m.Winner)
            .OrderByDescending(m => m.Date)
            .Take(count)
            .ToListAsync();
    }

    public async Task<Match?> Get(int id)
    {
        var match = await databaseContext.Matches
            .Include(m => m.Player1)
            .Include(m => m.Player2)
            .Include(m => m.Winner)
            .FirstOrDefaultAsync(x => x.Id == id);
        return match;
    }

    public async Task<Match> Update(Match match)
    {
        databaseContext.Matches.Update(match);
        await databaseContext.SaveChangesAsync();
        return match;
    }

    public async Task<int> Create(int player1Id, int player2Id)
    {
        var player1 = await databaseContext.Users.FindAsync(player1Id);
        var player2 = await databaseContext.Users.FindAsync(player2Id);
        if (player1 == null || player2 == null)
        {
            throw new ArgumentException("Player not found");
        }

        var match = new Match
        {
            Player1 = player1,
            Player2 = player2,
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