using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace SerioLogSample.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    public TestController()
    {
    }

    [HttpGet(nameof(TestLog))]
    public async Task<ActionResult> TestLog()
    {
        Log.Logger = new LoggerConfiguration()
    .WriteTo
    .MSSqlServer(
        connectionString: "Server=localhost,1433;Database=RepoDbSample;User Id=SA;Password=<YourStrong@Passw0rd>;Trusted_Connection=false;Persist Security Info=False;Encrypt=False;Integrated Security=SSPI;",
        sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents" })
    .CreateLogger();

    return Ok();
    }
}
