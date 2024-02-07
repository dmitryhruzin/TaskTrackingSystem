using BuisnessLogicLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Validation.Validators
{
    public class ProjectModelValidator : AbstractValidator<ProjectModel>
    {
        public ProjectModelValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(t => t.Description)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(500);

            RuleFor(t => t.StartDate)
                .NotNull();

            RuleFor(t => t.ExpiryDate)
                .NotNull()
                .GreaterThan(d => d.StartDate);
        }
    }
}
