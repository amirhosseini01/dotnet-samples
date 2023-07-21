using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteTest.TestingTools.Mocks;

public static class IdentityUserMock
{
    public static PageContext MoqClaimsPrincipal()
    {
        var ctx = new PageContext() { HttpContext = new DefaultHttpContext() };
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, "username"),
            new Claim(ClaimTypes.NameIdentifier, "userId"),
            new Claim("name", "John Doe"),
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        ctx.HttpContext.User = new ClaimsPrincipal(identity);

        return ctx;
    }
    public static PageContext MoqClaimsPrincipal(string userName, string userId)
    {
        var ctx = new PageContext() { HttpContext = new DefaultHttpContext() };
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim("name", "John Doe"),
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        ctx.HttpContext.User = new ClaimsPrincipal(identity);

        return ctx;
    }
}