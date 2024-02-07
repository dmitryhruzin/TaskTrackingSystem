using BuisnessLogicLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Validation.Validators
{
    public class PositionModelValidator : AbstractValidator<PositionModel>
    {
        public PositionModelValidator()
        {
            RuleFor(t=>t.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(t => t.Description)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(500);
        }
    }
}
