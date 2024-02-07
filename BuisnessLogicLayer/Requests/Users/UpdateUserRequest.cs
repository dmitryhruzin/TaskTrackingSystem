using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record UpdateUserRequest(
    int Id,
    string FirstName,
    string LastName,
    string UserName,
    string Email) : IRequest;