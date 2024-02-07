using BuisnessLogicLayer.Responses.Roles;
using MediatR;

namespace BuisnessLogicLayer.Requests.Roles;

public record GetRolesRequest() : IRequest<GetRolesResponse>;