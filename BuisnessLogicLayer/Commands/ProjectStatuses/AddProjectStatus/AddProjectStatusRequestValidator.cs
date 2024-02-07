using BuisnessLogicLayer.Requests.ProjectStatuses;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.ProjectStatuses.AddProjectStatus;

public class AddProjectStatusRequestValidator : AbstractValidator<AddProjectStatusRequest>
{
    public AddProjectStatusRequestValidator()
    {
        RuleFor(t=>t.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);
    }
}