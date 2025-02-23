using API.Models.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for seasons
/// </summary>
[Route("[controller]/")]
public class SeasonsController(ISeasonService seasonService) : ControllerBase
{
    /// <summary>
    /// Retrieves all seasons.
    /// </summary>
    /// <returns>All seasons</returns>
    [HttpGet("", Name = "GetAllSeasons")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<string>> GetAllSeasons()
    {
        var seasons = seasonService.GetSeasons();

        var seasonStrings = new List<string>();
        
        for (var i = seasons.Count; i > 0; i--)
        {
            seasonStrings.Add($"Season {i}");
        }
        
        return Ok(seasonStrings);
    }
}