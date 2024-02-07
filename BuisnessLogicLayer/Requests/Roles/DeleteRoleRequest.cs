using MediatR;

namespace BuisnessLogicLayer.Requests.Roles;

public record DeleteRoleRequest(int Id) : IRequest;