using Domain.Models;

namespace API.Models.Dtos;

/// <summary>
/// Data Transfer Object for News
/// </summary>
public class NewsDto
{
    /// <summary>
    /// Gets or sets the news content.
    /// </summary>
    public required string News { get; set; }
    
    /// <summary>
    /// Gets or sets the date of the news.
    /// </summary>
    public DateTime Date { get; set; }
}