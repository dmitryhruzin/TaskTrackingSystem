using MediatR;

namespace BuisnessLogicLayer.Requests.Roles;

public record AddRoleRequest(string Name) : IRequest;