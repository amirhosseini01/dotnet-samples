using Microsoft.AspNetCore.Mvc;
using Sentry;

namespace SentrySample.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet(nameof(Test2))]
    public IActionResult Test2()
    {
        return Ok();
    }
    [HttpGet(nameof(CaptureException))]
    public IActionResult CaptureException()
    {
        throw null;
    }

    [HttpGet(nameof(BadRequestRes))]
    public IActionResult BadRequestRes()
    {
        return BadRequest();
    }

    [HttpGet(nameof(UnauthorizedRes))]
    public IActionResult UnauthorizedRes()
    {
        return Unauthorized();
    }

    [HttpGet(nameof(NotFoundRes))]
    public IActionResult NotFoundRes()
    {
        return NotFound();
    }
}
