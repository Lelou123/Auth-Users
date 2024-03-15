using Microsoft.AspNetCore.Authorization;

namespace OrderFlow.Application.ApplicationServices.Policies;

public class UserAuthorizationRequirement(
    string[]? roles
) : IAuthorizationRequirement
{
    public string[]? Roles { get; set; } = roles;
}