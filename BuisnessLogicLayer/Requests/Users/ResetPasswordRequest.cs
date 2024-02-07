using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record ResetPasswordRequest(
    string Password,
    string ConfirmPassword,
    string Email,
    string Token) : IRequest;