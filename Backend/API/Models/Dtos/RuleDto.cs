using Domain.Models;

namespace API.Models.Dtos;

/// <summary>
/// Data Transfer Object for Rule
/// </summary>
public class RuleDto
{
    /// <summary>
    /// Gets or sets the ID of the rule.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the English text of the rule.
    /// </summary>
    public required string English { get; set; }

    /// <summary>
    /// Gets or sets the Danish text of the rule.
    /// </summary>
    public required string Danish { get; set; }

    /// <summary>
    /// Converts a Rule object to a RuleDto object.
    /// </summary>
    /// <param name="rule">The Rule object to convert.</param>
    /// <returns>A RuleDto object.</returns>
    public static implicit operator RuleDto(Rule rule) => new()
    {
        Id = rule.Id,
        English = rule.English,
        Danish = rule.Danish
    };
}