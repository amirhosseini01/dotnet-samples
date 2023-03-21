
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace SerioLogSample.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly Serilog.ILogger _logger;
    public TestController(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    [HttpGet(nameof(TestLog))]
    public async Task<ActionResult> TestLog()
    {
        _logger.Information("This is a log inside of the Minimal API endpoint.");

        return Ok();
    }
}
