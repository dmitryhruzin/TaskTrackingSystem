using BuisnessLogicLayer.Requests.Roles;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.Roles.UpdateRole;

public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
{
    public UpdateRoleRequestValidator()
    {
        RuleFor(t=>t.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);
    }
}