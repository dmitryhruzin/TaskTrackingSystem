using MediatR;

namespace BuisnessLogicLayer.Requests.AssignmentStatuses;

public record UpdateAssignmentStatusRequest(int Id, string Name) : IRequest;