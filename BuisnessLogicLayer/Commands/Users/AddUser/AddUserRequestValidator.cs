using BuisnessLogicLayer.Requests.Users;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.Users.AddUser;

public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
{
    public AddUserRequestValidator()
    {
        RuleFor(t=>t.FirstName)
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

        RuleFor(t=>t.Email)
            .NotEmpty()
            .EmailAddress()
            .MinimumLength(3)
            .MaximumLength(300);

        RuleFor(t => t.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(60)
            .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.");

        RuleFor(t=>t.ConfirmPassword)
            .NotEmpty()
            .Equal(t=>t.Password)
            .WithMessage("Passwords must match");
    }
}