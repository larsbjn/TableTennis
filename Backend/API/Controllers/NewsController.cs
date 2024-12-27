using API.Handlers;
using API.Hubs;
using API.Interfaces.Hubs;
using API.Models.Dtos;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers;

[Route("[controller]/[action]")]
public class NewsController(
    ILogger<NewsController> logger,
    IMatchRepository matchRepository,
    IUserRepository userRepository,
    RankingHandler rankingHandler,
    IHubContext<RankingHub, IRankingHub> rankingHub)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<NewsDto>>> GetLatest(int count)
    {
        var matches = await matchRepository.GetLatestWithNews(count);
        var news = (from match in matches where !string.IsNullOrEmpty(match.News) && match.Winner != null && match.Date != null select new NewsDto {News = match.News, Date = (DateTime)match.Date}).ToList();
        return Ok(news);
    }
}