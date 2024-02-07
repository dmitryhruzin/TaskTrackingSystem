using BuisnessLogicLayer.Requests.Projects;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.Projects.UpdateProject;

public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
{
    public UpdateProjectRequestValidator()
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