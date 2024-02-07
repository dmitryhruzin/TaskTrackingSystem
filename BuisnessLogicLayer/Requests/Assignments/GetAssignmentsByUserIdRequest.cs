using BuisnessLogicLayer.Responses.Assignments;
using MediatR;

namespace BuisnessLogicLayer.Requests.Assignments;

public record GetAssignmentsByUserIdRequest(int UserId) : IRequest<GetAssignmentsResponse>;