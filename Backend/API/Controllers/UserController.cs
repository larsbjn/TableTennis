using API.Models.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet(Name = "Get")]
    public async Task<ActionResult<UserDto>> Get(int id)
    {
        var user = await _userRepository.Get(id);
        if (user == null)
        {
            return NotFound();
        }

        return (UserDto)user;
    }
    
    [HttpGet(Name = "GetAll")]
    public async Task<IEnumerable<UserDto>> GetAll()
    {
        return (await _userRepository.GetAll()).Select(user => (UserDto)user);
    }
    
    [HttpPost(Name = "Add")]
    public async Task<ActionResult<User>> Add(UserDto user)
    {
        await _userRepository.Add(new User
        {
            Name = user.Name,
            Initials = user.Initials
        });
        return Created();
    }
    
    [HttpDelete(Name = "Delete")]
    public async Task<ActionResult<User>> Delete(int id)
    {
        await _userRepository.Delete(id);
        return Ok();
    }
}