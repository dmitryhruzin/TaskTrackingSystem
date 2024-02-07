using MediatR;

namespace BuisnessLogicLayer.Requests.Users;

public record DeleteUserRequest(int Id) : IRequest;