using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAll();
    public Task<User?> Get(int id);
    public Task Create(User user);
    public Task Update(User user);
    public Task Delete(int id);
}