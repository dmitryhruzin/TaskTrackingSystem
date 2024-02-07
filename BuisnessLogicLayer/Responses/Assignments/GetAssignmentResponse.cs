namespace BuisnessLogicLayer.Responses.Assignments;

public record GetAssignmentResponse(
    int Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime ExpiryDate,
    string? ManagerUserName,
    int? ManagerId,
    string StatusName,
    int StatusId,
    string ProjectName,
    int ProjectId,
    IReadOnlyCollection<int> UserProjectIds = default!);