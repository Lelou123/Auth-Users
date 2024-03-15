using OrderFlow.Domain.Abstractions;
using OrderFlow.Domain.Dtos;
using OrderFlow.Domain.Entities.Users;

namespace OrderFlow.Domain.Interfaces.Services;

public interface IJwtTokenService
{
    Result<JwtToken> GetToken(User user, IEnumerable<string> roles);
}