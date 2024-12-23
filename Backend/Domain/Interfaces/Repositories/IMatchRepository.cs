using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IMatchRepository
{
    public Task<IEnumerable<Match>> GetAll();
    public Task<Match?> Get(int id);
    public Task Add(Match user);
    public Task Delete(int id);
}