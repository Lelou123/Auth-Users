using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OrderFlow.Common.Domain.Entities.User;

namespace OrderFlow.Application.ApplicationServices.Policies;

public class UserAuthorizationHandler : AuthorizationHandler<UserAuthorizationRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;

    public UserAuthorizationHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    protected override async Task<Task> HandleRequirementAsync(
        AuthorizationHandlerContext context,
        UserAuthorizationRequirement requirement
    )
    {
        string? userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Sid);
        string? userRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

        if (userId is null || userRole is null)
        {
            return Task.CompletedTask;
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user is { IsActive: true })
        {
            return Task.CompletedTask;
        }

        if (requirement.Roles is not null && requirement.Roles.Contains(userRole))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}