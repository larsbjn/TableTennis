using API.Handlers;
using API.Interfaces.Hubs;
using API.Models.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class RankingHub : Hub<IRankingHub>
{
    public async Task UpdatedRanking(List<RankingDto> rankingDtos)
    {
        await Clients.All.UpdatedRanking(rankingDtos);
    }
}