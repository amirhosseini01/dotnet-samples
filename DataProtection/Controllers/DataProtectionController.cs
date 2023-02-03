using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace DataProtection.Controllers;

[ApiController]
[Route("[controller]")]
public class DataProtectionController : ControllerBase
{
    private readonly IDataProtector _protector;
    private const string _purpose = nameof(DataProtectionController);
    public DataProtectionController(IDataProtectionProvider provider)
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

    [HttpGet("UnProtectInput/{protectedText}/{purpose}")]
    public string UnProtectInput([FromServices] IDataProtectionProvider protectionProvider,
         string protectedText, string purpose)
    {
        var myProtector = GetProtector(provider: protectionProvider, purpose: purpose);
        return myProtector.Unprotect(protectedText);
    }

    [HttpGet("ProtectInput/{unprotectedText}/{purpose}")]
    public string ProtectInput([FromServices] IDataProtectionProvider protectionProvider,
         string unprotectedText, string purpose)
    {
        var myProtector = GetProtector(provider: protectionProvider, purpose: purpose);
        return myProtector.Protect(unprotectedText);
    }

    [HttpGet("HashPassword/{password}")]
    public string HashPassword(string password)
    {
        // Generate a 128-bit salt using a sequence of
        // cryptographically strong random bytes.
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return hashed;
    }
}
