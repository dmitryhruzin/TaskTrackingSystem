using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record ForgotPasswordRequest(
    string Email,
    string ClientUri): IRequest;