using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IRuleRepository
{
    public Task<IEnumerable<Rule>> GetAll();
    public Task<Rule?> Get(int id);
    public Task Create(Rule rule);
    public Task Update(Rule rule);
    public Task Delete(int id);
}