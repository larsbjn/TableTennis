using Domain.Models;

namespace API.Models.Dtos;

public class UserDto
{
    public string Name { get; set; }
    public string Initials { get; set; }
    
    public static implicit operator UserDto(User user) => new()
    {
        Name = user.Name,
        Initials = user.Initials
    };
}