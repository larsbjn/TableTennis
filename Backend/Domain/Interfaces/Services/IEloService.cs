using Domain.Models;

namespace Domain.Interfaces.Services;

public interface IEloService
{
    public Task CalculateElo(Match match);
}