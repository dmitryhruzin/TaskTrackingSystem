using BuisnessLogicLayer.Responses.Roles;
using MediatR;

namespace BuisnessLogicLayer.Requests.Roles;

public record GetRolesByUserIdRequest(int UserId) : IRequest<GetRolesResponse>;