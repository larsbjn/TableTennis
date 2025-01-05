using API.Models.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class IndexController()
    : ControllerBase
{
    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>A user object if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet]
    [Route("")]
    public ActionResult Index()
    {
        return Ok("Welcome to the API!");
    }
}