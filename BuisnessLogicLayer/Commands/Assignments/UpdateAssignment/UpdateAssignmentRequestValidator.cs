using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Assignments;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.Assignments.UpdateAssignment;

public class UpdateAssignmentRequestValidator : AbstractValidator<UpdateAssignmentRequest>
{
    public UpdateAssignmentRequestValidator (IProjectService projectService)
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
        
        RuleFor(t => t)
            .MustAsync(async (assignment, cancellation) =>
            {
                var project = await projectService.GetByIdAsync(assignment.ProjectId, cancellation);

                return project.StartDate <= assignment.StartDate && project.ExpiryDate >= assignment.ExpiryDate;
            })
            .WithMessage("The assignment start date must be after or on the project start date and the assignment end date must be before or on the project end date.");
    }
}