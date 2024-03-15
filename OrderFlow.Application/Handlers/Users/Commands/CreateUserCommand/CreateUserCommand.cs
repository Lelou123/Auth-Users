using OrderFlow.Application.Abstractions.Messaging;
using OrderFlow.Domain.Enums;

namespace OrderFlow.Application.Handlers.Users.Commands.CreateUserCommand;

public record CreateUserCommand(
    string FullName,
    string Email,
    DateTime BirthDate,
    CreateUserAddress Address
) : ICommand<Guid>;


public record CreateUserAddress(
    EnCountry Country,
    string City,
    string Street
);