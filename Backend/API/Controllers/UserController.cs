using API.Models.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController(ILogger<UserController> logger, IUserRepository userRepository)
    : ControllerBase
{
    [HttpGet(Name = "Get")]
    public async Task<ActionResult<UserDto>> Get(int id)
    {
        var user = await userRepository.Get(id);
        if (user == null)
        {
            return NotFound();
        }

        return (UserDto)user;
    }

    [HttpGet(Name = "GetAll")]
    public async Task<IEnumerable<UserDto>> GetAll()
    {
        return (await userRepository.GetAll()).Select(user => (UserDto)user);
    }

    [HttpPost(Name = "Add")]
    public async Task<ActionResult> Add(UserDto user)
    {
        await userRepository.Add(new User
        {
            Name = user.Name,
            Initials = user.Initials
        });
        return Created();
    }

    [HttpDelete(Name = "Delete")]
    public async Task<ActionResult<User>> Delete(int id)
    {
        await userRepository.Delete(id);
        return Ok();
    }
}