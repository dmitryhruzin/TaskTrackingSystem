using BuisnessLogicLayer.Requests.Users;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.Users.Login;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(t => t.Login)
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
    }
}