namespace Domain.Interfaces.Services;

public interface ISeasonService
{
    public List<int> GetSeasons();
    public int GetCurrentSeason();
    public (DateTime, DateTime) GetSeasonDates(int season);
}