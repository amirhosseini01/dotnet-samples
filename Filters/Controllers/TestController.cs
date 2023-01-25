using Filters.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Controllers;

[ApiController]
[Route("[controller]")]
[ResponseHeader("Filter-Header", "Filter Value")]
[First]
public class TestController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Short")]
    public IActionResult Short() =>
        Content($"- {nameof(WeatherForecastController)}.{nameof(Index)}");
}
