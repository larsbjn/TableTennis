using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IMatchRepository
{
    public Task<IEnumerable<Match>> GetAll();
    public Task<Match?> Get(int id);
    public Task Update(Match match);
    public Task<Match> UpdateWinner(int id, int winnerId);
    public Task<int> Create(int player1Id, int player2Id);
    public Task Delete(int id);
}