using BuisnessLogicLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Validation.Validators
{
    public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserModelValidator()
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
}
