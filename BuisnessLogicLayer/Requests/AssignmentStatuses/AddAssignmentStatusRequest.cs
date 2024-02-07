using MediatR;

namespace BuisnessLogicLayer.Requests.AssignmentStatuses;

public record AddAssignmentStatusRequest(string Name) : IRequest;