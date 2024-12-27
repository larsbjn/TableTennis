using API.Models.Dtos;

namespace API.Interfaces.Hubs;

public interface INewsHub
{
    Task UpdatedNews(List<NewsDto> news);
}