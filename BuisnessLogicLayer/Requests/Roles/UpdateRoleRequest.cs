using MediatR;

namespace BuisnessLogicLayer.Requests.Roles;

public record UpdateRoleRequest(int Id, string Name) : IRequest;