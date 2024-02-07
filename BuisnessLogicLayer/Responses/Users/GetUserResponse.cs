namespace BuisnessLogicLayer.Responses.Users;

public record GetUserResponse(
    int Id,
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    IReadOnlyCollection<int> UserProjectIds = default!,
    IReadOnlyCollection<int> TaskIds = default!);