using BuisnessLogicLayer.Responses.Roles;
using MediatR;

namespace BuisnessLogicLayer.Requests.Roles;

public record GetRoleRequest(int Id) : IRequest<GetRoleResponse>;