using BuisnessLogicLayer.Responses.Assignments;
using MediatR;

namespace BuisnessLogicLayer.Requests.Assignments;

public record GetAssignmentsByProjectIdRequest(int ProjectId) : IRequest<GetAssignmentsResponse>;