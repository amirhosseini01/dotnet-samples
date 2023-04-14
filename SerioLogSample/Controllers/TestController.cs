
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace SerioLogSample.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{    public TestController()
    {
    }

    [HttpGet(nameof(TestLog))]
    public async Task<ActionResult> TestLog()
    {
        Log.Information("123");
        var fruit = new[] { "Apple", "Pear", "Orange" };
Log.Information("In my bowl I have {Fruit}", fruit);

        return Ok();
    }
}
