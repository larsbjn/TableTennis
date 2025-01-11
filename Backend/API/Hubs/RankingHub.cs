using API.Handlers;
using API.Interfaces.Hubs;
using API.Models.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

/// <summary>
/// Hub for ranking updates
/// </summary>
public class RankingHub : Hub<IRankingHub>
{
    /// <summary>
    /// Notifies all clients of an updated ranking.
    /// </summary>
    /// <param name="rankingDtos">New rankings</param>
    public async Task UpdatedRanking(List<RankingDto> rankingDtos)
    {
        await Clients.All.UpdatedRanking(rankingDtos);
    }
}