using BuisnessLogicLayer.Responses.AssignmentStatuses;
using MediatR;

namespace BuisnessLogicLayer.Requests.Assignments;

public record UpdateAssignmentsStatusRequest(int Id, GetAssignmentStatusResponse Status) : IRequest;