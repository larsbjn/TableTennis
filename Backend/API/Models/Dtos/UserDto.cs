using Domain.Models;

namespace API.Models.Dtos;

/// <summary>
/// Data Transfer Object for User
/// </summary>
public class UserDto
{
    /// <summary>
    /// Gets or sets the ID of the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the initials of the user.
    /// </summary>
    public required string Initials { get; set; }

    /// <summary>
    /// Converts a User object to a UserDto object.
    /// </summary>
    /// <param name="user">The User object to convert.</param>
    /// <returns>A UserDto object.</returns>
    public static implicit operator UserDto(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Initials = user.Initials
    };
}