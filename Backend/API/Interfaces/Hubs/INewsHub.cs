using API.Models.Dtos;

namespace API.Interfaces.Hubs;

/// <summary>
/// Interface for broadcasting news updates.
/// </summary>
public interface INewsHub
{
    /// <summary>
    /// Broadcasts updated news to all connected clients.
    /// </summary>
    /// <param name="news">The list of news data transfer objects to broadcast.</param>
    Task UpdatedNews(List<NewsDto> news);
}