using API.Handlers;
using API.Models.Dtos;
using API.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for rankings
/// </summary>
/// <param name="matchRepository">The repository for accessing match data.</param>
/// <param name="seasonService">The service for managing season-related operations.</param>
[Route("[controller]/")]
public class RankingsController(
    IMatchRepository matchRepository,
    ISeasonService seasonService)
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
        var matches = await matchRepository.GetAll();
        var rankings = RankingHandler.GetRankings(matches.ToList());
        return Ok(rankings);
    }

    /// <summary>
    /// Retreive the rankings in a specific season
    /// </summary>
    /// <param name="season">The season number</param>
    /// <returns>Rankings for the season</returns>
    [HttpGet("{season}", Name = "GetRankingsForSeason")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RankingDto[]>> GetRankingsForSeason(int season)
    {
        var (startDate, endDate) = seasonService.GetSeasonDates(season);
        var matches = await matchRepository.GetBetweenDates(startDate, endDate);
        var rankings = RankingHandler.GetRankings(matches.ToList());
        return Ok(rankings);
    }
}