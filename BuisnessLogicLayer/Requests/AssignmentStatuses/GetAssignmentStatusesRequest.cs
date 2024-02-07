using BuisnessLogicLayer.Responses.AssignmentStatuses;
using MediatR;

namespace BuisnessLogicLayer.Requests.AssignmentStatuses;

public record GetAssignmentStatusesRequest() : IRequest<GetAssignmentStatusesResponse>;