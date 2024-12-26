using Domain.Interfaces.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class RankingHub : Hub<IRankingHub>
{
    public async Task UpdatedRanking(long username, string message) =>
            await Clients.All.UpdatedRanking(username, message);
}