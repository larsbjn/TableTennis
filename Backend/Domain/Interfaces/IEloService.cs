using Domain.Models;

namespace Domain.Interfaces;

public interface IEloService
{
    public Task CalculateElo(Match match);
}