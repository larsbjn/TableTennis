using API.Models.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class MatchController(
    ILogger<MatchController> logger,
    IMatchRepository matchRepository,
    IUserRepository userRepository)
    : ControllerBase
{

    [HttpGet(Name = "Get")]
    public async Task<ActionResult<MatchDto>> Get(int id)
    {
        var match = await matchRepository.Get(id);
        if (match == null)
        {
            return NotFound();
        }

        return (MatchDto)match;
    }

    [HttpGet(Name = "GetAll")]
    public async Task<IEnumerable<MatchDto>> GetAll()
    {
        return (await matchRepository.GetAll()).Select(match => (MatchDto)match);
    }

    [HttpPost(Name = "Add")]
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

    [HttpDelete(Name = "Delete")]
    public async Task<ActionResult> Delete(int id)
    {
        await matchRepository.Delete(id);
        return Ok();
    }
}