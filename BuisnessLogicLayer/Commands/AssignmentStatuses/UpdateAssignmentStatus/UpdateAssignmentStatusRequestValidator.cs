using BuisnessLogicLayer.Requests.AssignmentStatuses;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.AssignmentStatuses.UpdateAssignmentStatus;

public class UpdateAssignmentStatusRequestValidator : AbstractValidator<UpdateAssignmentStatusRequest>
{
    public UpdateAssignmentStatusRequestValidator()
    {
        RuleFor(t=>t.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);
    }
}