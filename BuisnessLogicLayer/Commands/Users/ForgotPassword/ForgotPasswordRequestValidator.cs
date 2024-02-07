using BuisnessLogicLayer.Requests.Users;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.Users.ForgotPassword;

public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordRequestValidator()
    {
        RuleFor(t => t.Email)
            .NotEmpty()
            .EmailAddress()
            .MinimumLength(3)
            .MaximumLength(300);

        RuleFor(t => t.ClientUri)
            .NotEmpty();
    }
}