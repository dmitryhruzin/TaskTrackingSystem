namespace BuisnessLogicLayer.Responses.Roles;

public record GetRolesResponse(IReadOnlyCollection<GetRoleResponse> Roles);