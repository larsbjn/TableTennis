using API.Models.Dtos;

namespace API.Interfaces.Hubs;

/// <summary>
/// Interface for the Ranking Hub.
/// </summary>
public interface IRankingHub
{
    /// <summary>
    /// Notifies clients that the ranking has been updated.
    /// </summary>
    /// <param name="rankings">The updated list of rankings.</param>
    Task UpdatedRanking(List<RankingDto> rankings);
}