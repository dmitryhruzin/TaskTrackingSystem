using BuisnessLogicLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Validation.Validators
{
    public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserModelValidator()
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
}
