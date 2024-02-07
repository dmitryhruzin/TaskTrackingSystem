using BuisnessLogicLayer.Models;
using FluentValidation;

namespace BuisnessLogicLayer.Validation.Validators
{
    internal class ForgotPasswordModelValidator : AbstractValidator<ForgotPasswordModel>
    {
        public ForgotPasswordModelValidator()
        {
            RuleFor(t => t.Email)
                .NotEmpty()
                .EmailAddress()
                .MinimumLength(3)
                .MaximumLength(300);

            RuleFor(t => t.ClientURI)
                .NotEmpty();
        }
    }
}
