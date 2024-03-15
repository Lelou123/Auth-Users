using OrderFlow.Application.Abstractions.Messaging;
using OrderFlow.Domain.Dtos;

namespace OrderFlow.Application.Handlers.Users.Commands.CreateUserCommand;

public record CreateUserCommand(
    string Name,
    string Email
) : ICommand<Result<CreateUserCommand>>;