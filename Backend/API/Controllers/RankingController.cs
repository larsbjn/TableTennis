using API.Handlers;
using API.Hubs;
using API.Interfaces.Hubs;
using API.Models.Dtos;
using Domain.Extensions;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers;

[Route("[controller]/[action]")]
public class RankingController(
    ILogger<RankingController> logger,
    IMatchRepository matchRepository,
    IUserRepository userRepository,
    RankingHandler rankingHandler,
    IHubContext<RankingHub, IRankingHub> rankingHub)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RankingDto[]>> GetAll()
    {
        var users = await userRepository.GetAll();
        var matches = await matchRepository.GetAll();
        var rankings = rankingHandler.GetRankings(matches.ToList(), users.ToList());
        return Ok(rankings);
    }
}