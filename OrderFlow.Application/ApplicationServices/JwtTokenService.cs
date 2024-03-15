using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OrderFlow.Common.CustomException;
using OrderFlow.Common.Domain.Entities.User;
using OrderFlow.Domain.Dtos;
using OrderFlow.Domain.Interfaces.Services;
using OrderFlow.Infrastructure.Settings;

namespace OrderFlow.Application.ApplicationServices;

public class JwtTokenService : IJwtTokenService
{
    public JwtToken GetToken(User user, IEnumerable<string> roles)
    {
        string? jwtSecret = JwtSecret.JwtSecretKey;

        if (jwtSecret is null)
        {
            throw new CustomException(HttpStatusCode.InternalServerError, "Jwt_Key Not found");
        }

        if (user.UserName is null)
        {
            throw new CustomException(HttpStatusCode.InternalServerError, "UserName cant be null");
        }

        var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Sid, user.Id.ToString())
            }
            .Concat(roles.Select(role => new Claim(ClaimTypes.Role, role)))
            .ToArray();


        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
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