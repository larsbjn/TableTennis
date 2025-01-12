using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IMatchRepository
{
    public Task<IEnumerable<Match>> GetAll();
    public Task<IEnumerable<Match>> GetLatestWithNews(int count);
    public Task<Match?> Get(int id);
    public Task<Match> Update(Match match);
    public Task<int> Create(int player1Id, int player2Id, NumberOfSets numberOfSets);
    public Task Delete(int id);
}