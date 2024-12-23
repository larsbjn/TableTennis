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
    /// <param name="match">The match object to add.</param>
    /// <returns>A 201 Created response if the match is successfully added; otherwise, a 400 Bad Request response.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Add(MatchDto match)
    {
        var player1 = await userRepository.Get(match.Player1);
        var player2 = await userRepository.Get(match.Player2);

        if (player1 == null || player2 == null)
        {
            return BadRequest();
        }

        var winner = match.Winner == match.Player1 ? player1 : match.Winner == match.Player2 ? player2 : null;
        if (winner == null)
        {
            return BadRequest();
        }

        await matchRepository.Add(new Match
        {
            Player1 = player1,
            Player2 = player2,
            Winner = winner,
            Date = match.Date,
            News = match.News,
            ExtraInfo1 = match.ExtraInfo1,
            ExtraInfo2 = match.ExtraInfo2
        });
        return Created();
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