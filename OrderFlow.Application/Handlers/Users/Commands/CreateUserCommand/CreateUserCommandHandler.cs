using System.Net;
using OrderFlow.Application.Abstractions.Messaging;
using OrderFlow.Common.CustomException;
using OrderFlow.Domain.Dtos;

namespace OrderFlow.Application.Handlers.Users.Commands.CreateUserCommand;

internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserCommand>
{
    public async Task<Result<CreateUserCommand>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Name is "Jorge")
        {
            throw new CustomException(HttpStatusCode.Forbidden, "Something went wrong");
        }

        return new Result<CreateUserCommand> {
            Data = request,
            IsSuccess = true
        };
    }
}