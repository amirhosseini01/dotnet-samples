using Filters.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet(Name = "Short")]
    [ShortCircuitingResourceFilter]
    public IActionResult Short() =>
        Content($"- {nameof(WeatherForecastController)}.{nameof(Index)}");
}
