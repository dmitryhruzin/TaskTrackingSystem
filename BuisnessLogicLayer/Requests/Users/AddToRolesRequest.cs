using BuisnessLogicLayer.Responses.Roles;
using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record AddToRolesRequest(int Id, IReadOnlyCollection<GetRoleResponse> Roles) : IRequest;