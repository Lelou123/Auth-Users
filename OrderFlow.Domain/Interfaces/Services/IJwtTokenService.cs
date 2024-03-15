using OrderFlow.Common.Domain.Entities.User;
using OrderFlow.Domain.Dtos;

namespace OrderFlow.Domain.Interfaces.Services;

public interface IJwtTokenService
{
    JwtToken GetToken(User user, IEnumerable<string> roles);
}