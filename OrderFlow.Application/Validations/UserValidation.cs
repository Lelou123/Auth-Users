using FluentValidation;
using OrderFlow.Common.Domain.Entities.User;

namespace OrderFlow.Application.Validations;

public class UserValidation : AbstractValidator<User>
{
    public UserValidation()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.UserName).NotEmpty();
    }
}