using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MatchRepository(DatabaseContext databaseContext) : IMatchRepository
{
    public async Task<IEnumerable<Match>> GetAll()
    {
        return await databaseContext.Matches.ToListAsync();
    }

    public async Task<Match?> Get(int id)
    {
        var match = await databaseContext.Matches.FirstOrDefaultAsync(x => x.Id == id);
        return match;
    }

    public async Task Add(Match user)
    {
        await databaseContext.Matches.AddAsync(user);
        await databaseContext.SaveChangesAsync();
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