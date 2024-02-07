namespace BuisnessLogicLayer.Responses.Positions;

public record GetPositionsResponse(
    IReadOnlyCollection<GetPositionResponse> Positions);