using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Pages.Books;

namespace Site.Helpers.Identity;

public static class AuthorizationHelper
{
    public static async Task<AuthorizationResult> AuthorizeUser(this IAuthorizationService authorizationService, ClaimsPrincipal user, string policyName)
    {
        return await authorizationService
            .AuthorizeAsync(user: user, resource: null, policyName: policyName);
    }

    public static IActionResult ReturnForbiddenResult()
    {
        return new ForbidResult();   
    }
}