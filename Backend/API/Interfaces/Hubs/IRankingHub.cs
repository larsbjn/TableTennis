using API.Models.Dtos;

namespace API.Interfaces.Hubs;

public interface IRankingHub
{
    Task UpdatedRanking(List<RankingDto> rankings);
}