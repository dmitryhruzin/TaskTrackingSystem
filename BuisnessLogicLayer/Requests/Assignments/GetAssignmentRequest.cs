using BuisnessLogicLayer.Responses.Assignments;
using MediatR;

namespace BuisnessLogicLayer.Requests.Assignments;

public record GetAssignmentRequest(int Id) : IRequest<GetAssignmentResponse>;