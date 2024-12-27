using Domain.Models;

namespace API.Models.Dtos;

public class RuleDto
{
    public int Id { get; set; }
    public string English { get; set; }
    public string Danish { get; set; }
    
    public static implicit operator RuleDto(Rule rule) => new()
    {
        Id = rule.Id,
        English = rule.English,
        Danish = rule.Danish
    };
}