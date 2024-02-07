using MediatR;

namespace BuisnessLogicLayer.Requests.Assignments;

public record UpdateAssignmentRequest(
    int Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime ExpiryDate,
    int? ManagerId,
    int StatusId,
    int ProjectId) : IRequest;