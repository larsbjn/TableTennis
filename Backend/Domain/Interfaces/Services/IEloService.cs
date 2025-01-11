using Domain.Models;

namespace Domain.Interfaces.Services;

public interface IEloService
{
    public Task AdjustEloForMatch(Match match);
}