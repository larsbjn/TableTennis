using API.Handlers;
using API.Models.Dtos;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for rankings
/// </summary>
/// <param name="matchRepository"></param>
/// <param name="userRepository"></param>
[Route("[controller]/")]
public class RankingsController(
    IMatchRepository matchRepository,
    IUserRepository userRepository)
    : ControllerBase
{
    /// <summary>
    /// Retrieves all rankings.
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetAllRankings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RankingDto[]>> GetAll()
    {
        var users = await userRepository.GetAll();
        var matches = await matchRepository.GetAll();
        var rankings = RankingHandler.GetRankings(matches.ToList(), users.ToList());
        return Ok(rankings);
    }
}