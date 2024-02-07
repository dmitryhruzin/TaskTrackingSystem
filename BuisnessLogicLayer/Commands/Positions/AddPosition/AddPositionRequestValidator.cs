using BuisnessLogicLayer.Requests.Positions;
using FluentValidation;

namespace BuisnessLogicLayer.Commands.Positions.AddPosition;

public class AddPositionRequestValidator : AbstractValidator<AddPositionRequest>
{
    public AddPositionRequestValidator()
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