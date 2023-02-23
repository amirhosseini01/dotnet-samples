using Autofac;
using AutofacSample.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutofacSample.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly SingleService _singleService;
    private readonly ScopeService _scopeService;
    private readonly TransientService _transientService;
    public TestController(SingleService singleService,
        ScopeService scopeService,
        TransientService transientService
        )
    {
        _singleService = singleService;
        _scopeService = scopeService;
        _transientService = transientService;
    }

    [HttpGet(nameof(GetAllDI))]
    public IEnumerable<string> GetAllDI()
    {
        yield return _singleService.GetNewGUID();
        yield return _scopeService.GetNewGUID();
        yield return _transientService.GetNewGUID();
    }

    [HttpGet(nameof(GetSingle))]
    public string GetSingle()
    {
        return _singleService.GetNewGUID();
    }

    [HttpGet(nameof(GetScoped))]
    public string GetScoped()
    {
        return _scopeService.GetNewGUID();
    }

    [HttpGet(nameof(GetTransient))]
    public string GetTransient()
    {
        return _transientService.GetNewGUID();
    }
}
