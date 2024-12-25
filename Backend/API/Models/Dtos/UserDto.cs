using Domain.Models;

namespace API.Models.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Initials { get; set; }
    
    public static implicit operator UserDto(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Initials = user.Initials
    };
}