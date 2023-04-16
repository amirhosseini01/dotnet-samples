using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using SieveApiSample.Data;

namespace SieveApiSample.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly SieveProcessor _sieveProcessor;
    private readonly ApplicationContext _context;
    public TestController(SieveProcessor sieveProcessor,
        ApplicationContext context)
    {
        _sieveProcessor = sieveProcessor;
        _context = context;
    }

    [HttpGet(nameof(GetPosts))]
    public IActionResult GetPosts(SieveModel sieveModel)
    {
        var query = _context.Posts.AsNoTracking(); // Makes read-only queries faster
        query = _sieveProcessor.Apply(sieveModel, query); // Returns `result` after applying the sort/filter/page query in `SieveModel` to it

        var result = query.ToList();
        return Ok(result);
    }


}
