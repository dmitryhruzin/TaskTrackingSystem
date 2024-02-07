using BuisnessLogicLayer.Requests.Users;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.Users.UpdateUser;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(t => t.FirstName)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(30);

        RuleFor(t => t.LastName)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(30);

        RuleFor(t => t.UserName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(300);

        RuleFor(t => t.Email)
            .NotEmpty()
            .EmailAddress()
            .MinimumLength(3)
            .MaximumLength(300);
    }
}