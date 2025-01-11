using API.Interfaces.Hubs;
using API.Models.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

/// <summary>
/// Hub for broadcasting news updates.
/// </summary>
public class NewsHub : Hub<INewsHub>
{
    /// <summary>
    /// Broadcasts updated news to all connected clients.
    /// </summary>
    /// <param name="newsDtos">The list of news data transfer objects to broadcast.</param>
    public async Task UpdatedNews(List<NewsDto> newsDtos)
    {
        await Clients.All.UpdatedNews(newsDtos);
    }
}