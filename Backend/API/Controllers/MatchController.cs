using API.Models.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]/[action]")]
public class MatchController(
    ILogger<MatchController> logger,
    IMatchRepository matchRepository,
    IUserRepository userRepository)
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
    /// Updates an existing match.
    /// </summary>
    /// <param name="match">The match data to update.</param>
    /// <returns>An OK response if the match is successfully updated; otherwise, a 400 Bad Request response.</returns>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(MatchDto match)
    {
        try
        {
            var entity = await matchRepository.Get(match.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.News = match.News;
            entity.ExtraInfo1 = match.ExtraInfo1;
            entity.ExtraInfo2 = match.ExtraInfo2;
            await matchRepository.Update(entity);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Update the winner of a match
    /// </summary>
    /// <param name="matchId">The match id</param>
    /// <param name="winnerId">The winning player id</param>
    /// <returns></returns>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MatchDto>> UpdateWinner(int matchId, int winnerId)
    {
        try
        {
            var match = await matchRepository.UpdateWinner(matchId, winnerId);
            return Ok((MatchDto)match);
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