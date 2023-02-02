using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace DataProtection.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IDataProtector _protector;
    private const string _purpose = nameof(TestController);
    public TestController(IDataProtectionProvider provider)
    {
        _protector = GetProtector(provider: provider, purpose: _purpose);
    }
    private static IDataProtector GetProtector(IDataProtectionProvider provider, string purpose)
    {
        return provider.CreateProtector(purpose: purpose);
    }

    [HttpGet("SimpleProtection")]
    public Dictionary<string, string> SimpleProtection()
    {
        const string output = "Hello World!";

        string protectedOutPut = _protector.Protect(output);
        Dictionary<string, string> response = new()
        {
            {"purpose: ",_purpose},
            {"some output to protect: ", output},
            {"protected shape: ", protectedOutPut},
            {"un-protected shape: ", _protector.Unprotect(protectedOutPut)},
        };

        return response;
    }

    [HttpGet("UnProtectInput/{protectedText}")]
    public string UnProtectInput(string protectedText)
    {
        return _protector.Unprotect(protectedText);
    }

    [HttpGet("UnProtectInput/{protectedText}/{provider}")]
    public string UnProtectInput([FromServices] IDataProtectionProvider protectionProvider,
         string protectedText, string provider)
    {
        var myProtector = GetProtector(provider: protectionProvider, purpose: provider);
        return myProtector.Unprotect(protectedText);
    }

    [HttpGet("ProtectInput/{UnprotectedText}/{provider}")]
    public string ProtectInput([FromServices] IDataProtectionProvider protectionProvider,
         string UnprotectedText, string provider)
    {
        var myProtector = GetProtector(provider: protectionProvider, purpose: provider);
        return myProtector.Protect(UnprotectedText);
    }
}
