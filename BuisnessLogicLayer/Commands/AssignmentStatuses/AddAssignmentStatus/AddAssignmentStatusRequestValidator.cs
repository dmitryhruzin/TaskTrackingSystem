using BuisnessLogicLayer.Requests.AssignmentStatuses;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.AssignmentStatuses.AddAssignmentStatus;

public class AddAssignmentStatusRequestValidator : AbstractValidator<AddAssignmentStatusRequest>
{
    public AddAssignmentStatusRequestValidator()
    {
        RuleFor(t=>t.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);
    }
}