using BuisnessLogicLayer.Responses.AssignmentStatuses;
using MediatR;

namespace BuisnessLogicLayer.Requests.AssignmentStatuses;

public record GetAssignmentStatusRequest(int Id) : IRequest<GetAssignmentStatusResponse>;