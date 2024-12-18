using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAll();
    public Task<User?> Get(int id);
    public Task Add(User user);
    public Task Delete(int id);
}