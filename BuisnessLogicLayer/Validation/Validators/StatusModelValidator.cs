using BuisnessLogicLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Validation.Validators
{
    public class StatusModelValidator : AbstractValidator<StatusModel>
    {
        public StatusModelValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}
