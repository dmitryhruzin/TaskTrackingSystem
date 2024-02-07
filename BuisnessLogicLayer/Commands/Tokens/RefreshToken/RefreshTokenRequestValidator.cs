using BuisnessLogicLayer.Requests.Tokens;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.Tokens.RefreshToken;

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(t => t.AccessToken)
            .NotEmpty();

        RuleFor(t => t.RefreshToken)
            .NotEmpty();
    }
}