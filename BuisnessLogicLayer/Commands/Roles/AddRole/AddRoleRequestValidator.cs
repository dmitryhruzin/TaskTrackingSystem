using BuisnessLogicLayer.Requests.Roles;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.Roles.AddRole;

public class AddRoleRequestValidator : AbstractValidator<AddRoleRequest>
{
    public AddRoleRequestValidator()
    {
        RuleFor(t=>t.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);
    }
}