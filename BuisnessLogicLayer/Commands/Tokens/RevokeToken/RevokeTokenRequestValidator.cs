using BuisnessLogicLayer.Requests.Tokens;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.Tokens.RevokeToken;

public class RevokeTokenRequestValidator : AbstractValidator<RevokeTokenRequest>
{
    public RevokeTokenRequestValidator()
    {
        RuleFor(t => t.AccessToken)
            .NotEmpty();
    }
}