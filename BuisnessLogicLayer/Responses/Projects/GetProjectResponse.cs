namespace BuisnessLogicLayer.Responses.Projects;

public record GetProjectResponse(
    int Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime ExpiryDate,
    string StatusName,
    int StatusId,
    IReadOnlyCollection<int> TaskIds = default!);