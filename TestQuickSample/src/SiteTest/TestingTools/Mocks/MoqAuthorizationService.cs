using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SiteTest.TestingTools.Mocks;

public class MoqAuthorizationService : IAuthorizationService
{
    private readonly bool _returnsSuccessAuthorizeResult;
    public MoqAuthorizationService(bool returnsSuccessAuthorizeResult)
    {
        _returnsSuccessAuthorizeResult = returnsSuccessAuthorizeResult;
    }

    public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, IEnumerable<IAuthorizationRequirement> requirements)
    {
        if (_returnsSuccessAuthorizeResult)
            return Task.FromResult(AuthorizationResult.Success());

        return Task.FromResult(AuthorizationResult.Failed());
    }

    public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, string policyName)
    {
        if (_returnsSuccessAuthorizeResult)
            return Task.FromResult(AuthorizationResult.Success());

        return Task.FromResult(AuthorizationResult.Failed());
    }
}