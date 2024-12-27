using API.Handlers;
using API.Hubs;
using API.Interfaces.Hubs;
using API.Models.Dtos;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers;

[Route("[controller]/[action]")]
public class MatchController(
    ILogger<MatchController> logger,
    IMatchRepository matchRepository,
    IUserRepository userRepository,
    RankingHandler rankingHandler,
    IEloService eloService,
    IHubContext<RankingHub, IRankingHub> rankingHub,
    IHubContext<NewsHub, INewsHub> newsHub)
    : ControllerBase
{
    /// <summary>
    /// Retrieves a match by its ID.
    /// </summary>
    /// <param name="id">The ID of the match to retrieve.</param>
    /// <returns>A match object if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MatchDto>> Get(int id)
    {
        var match = await matchRepository.Get(id);
        if (match == null)
        {
            return NotFound();
        }

        return (MatchDto)match;
    }


    /// <summary>
    /// Updates a match with the provided details.
    /// </summary>
    /// <param name="updateMatchDto">The updated info for the match</param>
    /// <returns>An updated match object if successful; otherwise, a 400 Bad Request response.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MatchDto>> Update(UpdateMatchDto updateMatchDto)
    {
        try
        {
            var winner = await userRepository.Get(updateMatchDto.WinnerId ?? 0);

            if (winner == null) return BadRequest();
            
            var match = await matchRepository.Get(updateMatchDto.Id);
            if (match == null) return BadRequest();
            
            match.Winner = winner;
            match.News = updateMatchDto.News;
            match.ExtraInfo1 = updateMatchDto.ExtraInfo1;
            match.ExtraInfo2 = updateMatchDto.ExtraInfo2;
            
            var updatedMatch = await matchRepository.Update(match);

            if (updateMatchDto.UpdateWinner)
            {
                await eloService.CalculateElo(match);

                // Update rankings
                var matches = await matchRepository.GetAll();
                var users = await userRepository.GetAll();
                var rankings = rankingHandler.GetRankings(matches.ToList(), users.ToList());
                await rankingHub.Clients.All.UpdatedRanking(rankings);
            }
            
            // Update news
            var latestMatches = await matchRepository.GetLatestWithNews(5);
            var news = latestMatches.Select(m => new NewsDto{ News = m.News ?? "", Date = m.Date ?? DateTime.Now }).ToList();
            await newsHub.Clients.All.UpdatedNews(news.ToList());
            return Ok(updatedMatch);
        }
        catch (ArgumentException e)
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Retrieves all matches.
    /// </summary>
    /// <returns>A list of all matches.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<MatchDto>> GetAll()
    {
        return (await matchRepository.GetAll()).Select(match => (MatchDto)match);
    }

    /// <summary>
    /// Adds a new match.
    /// </summary>
    /// <param name="player1Id">Player 1 id</param>
    /// <param name="player2Id">Player 2 id</param>
    /// <returns>A 201 Created response if the match is successfully added; otherwise, a 400 Bad Request response.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(int player1Id, int player2Id)
    {
        try
        {
            var match = await matchRepository.Create(player1Id, player2Id);
            return Created($"/match/{match}", match);
        }
        catch (ArgumentException e)
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Deletes a match by its ID.
    /// </summary>
    /// <param name="id">The ID of the match to delete.</param>
    /// <returns>A 200 OK response if the match is successfully deleted.</returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(int id)
    {
        await matchRepository.Delete(id);
        return Ok();
    }
}