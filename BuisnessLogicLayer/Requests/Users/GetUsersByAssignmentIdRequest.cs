using BuisnessLogicLayer.Responses.Users;
using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record GetUsersByAssignmentIdRequest(int Id) : IRequest<GetUsersResponse>;