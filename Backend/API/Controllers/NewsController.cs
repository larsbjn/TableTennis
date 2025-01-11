using API.Handlers;
using API.Hubs;
using API.Interfaces.Hubs;
using API.Models.Dtos;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers;

/// <summary>
/// Controller for news
/// </summary>
/// <param name="matchRepository"></param>
[Route("[controller]/")]
public class NewsController(
    IMatchRepository matchRepository)
    : ControllerBase
{
    /// <summary>
    /// Retrieves the latest news.
    /// </summary>
    /// <param name="count">Number of latest news</param>
    /// <returns>Latest news</returns>
    [HttpGet("latest/{count:int}", Name = "GetLatestNews")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<NewsDto>>> Latest(int count)
    {
        var matches = await matchRepository.GetLatestWithNews(count);
        var news = (from match in matches where !string.IsNullOrEmpty(match.News) && match.Winner != null && match.Date != null select new NewsDto {News = match.News!, Date = (DateTime)match.Date!}).ToList();

        return Ok(news);
    }
}