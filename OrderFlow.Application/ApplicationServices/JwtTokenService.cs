using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderFlow.Domain.Abstractions;
using OrderFlow.Domain.Dtos;
using OrderFlow.Domain.Entities.Users;
using OrderFlow.Domain.Interfaces.Services;
using OrderFlow.Infrastructure.Settings;

namespace OrderFlow.Application.ApplicationServices;

public class JwtTokenService(
    IOptions<JwtSecret> jwtSecret
) : IJwtTokenService
{
    private readonly JwtSecret? _jwtSecret = jwtSecret.Value;

    public Result<JwtToken> GetToken(User user, IEnumerable<string> roles)
    {

        if (_jwtSecret is null)
        {
            return Result.Failure<JwtToken>(
                new Error("JwtKey.NotFound", "The Jwt Key was not found, verify the configurations")
            );
        }

        if (user.UserName is null)
        {
            return Result.Failure<JwtToken>(Error.NullValue);
        }

        Claim[]? userClaims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Sid, user.Id.ToString())
            }
            .Concat(roles.Select(role => new Claim(ClaimTypes.Role, role)))
            .ToArray();


        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret.JwtSecretKey));
        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            claims: userClaims,
            signingCredentials: credentials,
            expires: DateTime.UtcNow.AddHours(24)
        );

        string? jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return new JwtToken(jwtToken);
    }
}