using BuisnessLogicLayer.Responses.Assignments;
using MediatR;

namespace BuisnessLogicLayer.Requests.Assignments;

public record GetAssignmentsRequest() : IRequest<GetAssignmentsResponse>;