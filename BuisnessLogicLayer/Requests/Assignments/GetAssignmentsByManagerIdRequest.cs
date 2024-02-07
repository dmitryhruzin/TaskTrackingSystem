using BuisnessLogicLayer.Responses.Assignments;
using MediatR;

namespace BuisnessLogicLayer.Requests.Assignments;

public record GetAssignmentsByManagerIdRequest(int ManagerId) : IRequest<GetAssignmentsResponse>;