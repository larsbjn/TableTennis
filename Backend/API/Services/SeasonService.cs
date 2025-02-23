using Domain.Interfaces.Services;

namespace API.Services;

/// <summary>
/// Service for seasons
/// </summary>
public class SeasonService : ISeasonService
{
    private static readonly DateTime StartDate = new(2024, 1, 1);

    /// <summary>
    /// Get all seasons
    /// </summary>
    /// <returns>All seasons</returns>
    public List<int> GetSeasons()
    {
        var seasons = new List<int>();
        var currentDate = DateTime.Now;
        var monthsDifference = (currentDate.Year - StartDate.Year) * 12 + currentDate.Month - StartDate.Month;
        var seasonCount = monthsDifference / 3 + 1;

        for (var i = 1; i <= seasonCount; i++)
        {
            seasons.Add(i);
        }

        return seasons;
    }

    /// <summary>
    /// Retrieves the current season
    /// </summary>
    /// <returns>The current season</returns>
    public int GetCurrentSeason()
    {
        var currentDate = DateTime.Now;
        var monthsDifference = (currentDate.Year - StartDate.Year) * 12 + currentDate.Month - StartDate.Month;
        return monthsDifference / 3 + 1;
    }

    /// <summary>
    /// Finds the start and end date of a season
    /// </summary>
    /// <param name="season">The season to find start and end dates for</param>
    /// <returns>Start and end date of the season</returns>
    public (DateTime, DateTime) GetSeasonDates(int season)
    {
        var startDate = StartDate.AddMonths((season - 1) * 3);
        var endDate = startDate.AddMonths(3).AddDays(-1);
        return (startDate, endDate);
    }
}