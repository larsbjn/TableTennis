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
    public async Task NotifyAboutUpdatedRanking()
    {
        await Clients.All.NotifyAboutUpdatedRanking();
    }
}