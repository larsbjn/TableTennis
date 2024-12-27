using API.Interfaces.Hubs;
using API.Models.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class NewsHub : Hub<INewsHub>
{
    public async Task UpdatedNews(List<NewsDto> newsDtos)
    {
        await Clients.All.UpdatedNews(newsDtos);
    }
}