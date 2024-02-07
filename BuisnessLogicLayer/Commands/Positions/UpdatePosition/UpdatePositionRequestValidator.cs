using BuisnessLogicLayer.Requests.Positions;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.Positions.UpdatePosition;

public class UpdatePositionRequestValidator : AbstractValidator<UpdatePositionRequest>
{
    public UpdatePositionRequestValidator()
    {
        RuleFor(t=>t.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);

        RuleFor(t => t.Description)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(500);
    }
}