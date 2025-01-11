using System.Buffers.Text;
using Microsoft.AspNetCore.Mvc;

namespace ByteArray.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpPost("upload")]
    public IActionResult UploadFile([FromBody] FileUploadModel model)
    {
        var data = model.Data; // This is already a byte array
        var fileName = model.FileName;
        var description = model.Description;

        var dataString = Convert.ToBase64String(data);
        var text = Base64.DecodeFromUtf8(data);
        // Process the data, fileName, and description
        return Ok();
    }
}