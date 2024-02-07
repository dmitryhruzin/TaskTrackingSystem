using BuisnessLogicLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Validation.Validators
{
    public class TokenModelValidator : AbstractValidator<TokenModel>
    {
        public TokenModelValidator()
        {
            RuleFor(t => t.AccessToken)
                .NotEmpty();

            RuleFor(t => t.RefreshToken)
                .NotEmpty();
        }
    }
}
