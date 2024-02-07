namespace BuisnessLogicLayer.Responses.Positions;

public record GetPositionResponse(
    int Id,
    string Name,
    string Description);