namespace Domain.Interfaces.Hubs;

public interface IRankingHub
{
    Task UpdatedRanking(long username, string message);
}