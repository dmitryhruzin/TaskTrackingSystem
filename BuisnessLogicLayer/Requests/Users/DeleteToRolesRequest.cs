using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record DeleteToRolesRequest(int Id, IReadOnlyCollection<int> RoleIds) : IRequest;