using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(DatabaseContext databaseContext) : IUserRepository
{
    public async Task<IEnumerable<User>> GetAll()
    {
        return await databaseContext.Users.Include(u => u.Players).ToListAsync();
    }

    public async Task<User?> Get(int id)
    {
        var user = await databaseContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public async Task Create(User user)
    {
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        databaseContext.Users.Update(user);
        await databaseContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var user = await databaseContext.Users.FindAsync(id);
        if (user != null)
        {
            databaseContext.Users.Remove(user);
            await databaseContext.SaveChangesAsync();
        }
    }
}