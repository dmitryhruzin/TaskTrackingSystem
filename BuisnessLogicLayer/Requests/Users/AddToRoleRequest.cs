using BuisnessLogicLayer.Responses.Roles;
using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record AddToRoleRequest(int Id, GetRoleResponse Role) : IRequest;