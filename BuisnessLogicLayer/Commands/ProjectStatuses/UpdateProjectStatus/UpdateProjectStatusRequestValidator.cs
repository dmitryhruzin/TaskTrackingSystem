using BuisnessLogicLayer.Requests.ProjectStatuses;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.ProjectStatuses.UpdateProjectStatus;

public class UpdateProjectStatusRequestValidator : AbstractValidator<UpdateProjectStatusRequest>
{
    public UpdateProjectStatusRequestValidator()
    {
        RuleFor(t=>t.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);
    }
}