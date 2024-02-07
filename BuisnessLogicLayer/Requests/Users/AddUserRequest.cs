using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record AddUserRequest(
    string FirstName,
    string LastName,
    string UserName,
    string Password,
    string ConfirmPassword,
    string Email,
    int? RoleId) : IRequest;