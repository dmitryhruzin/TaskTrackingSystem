using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record DeleteToRoleRequest(int Id, int RoleId) : IRequest;