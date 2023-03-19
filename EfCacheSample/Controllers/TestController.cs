using EfCacheSample.Data;
using EfCacheSample.Models;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.AspNetCore.Mvc;

namespace EfCacheSample.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly MainContext _mainContext;
    public TestController(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    [HttpGet(nameof(GetPersons))]
    public ActionResult<List<Person>> GetPersons()
    {
        var persons = _mainContext.Persons
                   .Where(x => x.Id > 0)
                   .OrderBy(x => x.Id)
                   .Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(5))
                   .FirstOrDefault();

        return Ok(persons);
    }
}
