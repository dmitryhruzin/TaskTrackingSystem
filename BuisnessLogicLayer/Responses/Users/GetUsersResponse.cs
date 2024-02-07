namespace BuisnessLogicLayer.Responses.Users;

public record GetUsersResponse(IReadOnlyCollection<GetUserResponse> Users);