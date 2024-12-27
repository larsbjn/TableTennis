using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RuleRepository(DatabaseContext databaseContext) : IRuleRepository
{
    public async Task<Rule?> Get(int id)
    {
        return await databaseContext.Rules.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Create(Rule rule)
    {
        await databaseContext.Rules.AddAsync(rule);
        await databaseContext.SaveChangesAsync();
    }

    public async Task Update(Rule rule)
    {
        databaseContext.Rules.Update(rule);
        await databaseContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Rule>> GetAll()
    {
        return await databaseContext.Rules.ToListAsync();
    }

    public async Task Delete(int id)
    {
        var rule = await databaseContext.Rules.FindAsync(id);
        if (rule != null)
        {
            databaseContext.Rules.Remove(rule);
            await databaseContext.SaveChangesAsync();
        }
    }
}