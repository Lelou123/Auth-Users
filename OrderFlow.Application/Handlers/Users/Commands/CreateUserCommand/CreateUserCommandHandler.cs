using Microsoft.AspNetCore.Identity;
using OrderFlow.Application.Abstractions.Messaging;
using OrderFlow.Domain.Abstractions;
using OrderFlow.Domain.Dtos;
using OrderFlow.Domain.Entities.Users;
using OrderFlow.Domain.Interfaces.Services;

namespace OrderFlow.Application.Handlers.Users.Commands.CreateUserCommand;

internal class CreateUserCommandHandler(
    UserManager<User> userManager,
    IMappingService mappingService,
    IEmailService emailService
) : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = mappingService.Map<User>(request);

        var createUserResult = await userManager.CreateAsync(user);

        if (!createUserResult.Succeeded)
        {
            return Result.Failure<Guid>(
                new Error("User.CreateUser", $"Error While Creating User: {createUserResult.Errors.FirstOrDefault()}")
            );
        }

        var sendEmailResult = await emailService.SendEmail(
            new EmailMessage(user.Email, user.UserName, "Activate Email", "Email Activation")
        );

        if (sendEmailResult.IsFailure)
        {
            return Result.Failure<Guid>(sendEmailResult.Error);
        }


        return Guid.NewGuid();
    }
}