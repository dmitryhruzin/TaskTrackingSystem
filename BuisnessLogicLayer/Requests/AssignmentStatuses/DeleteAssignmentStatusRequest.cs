using MediatR;

namespace BuisnessLogicLayer.Requests.AssignmentStatuses;

public record DeleteAssignmentStatusRequest(int Id) : IRequest;