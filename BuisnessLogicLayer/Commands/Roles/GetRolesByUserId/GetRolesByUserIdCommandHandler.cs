using System.Linq.Expressions;
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Roles;
using BuisnessLogicLayer.Responses.Roles;
using MediatR;

namespace BuisnessLogicLayer.Commands.Roles.GetRolesByUserId;

public class GetRolesByUserIdCommandHandler : IRequestHandler<GetRolesByUserIdRequest, GetRolesResponse>
{
    private readonly IMapper _mapper;
    private readonly IRoleService _roleService;

    public GetRolesByUserIdCommandHandler(
        IMapper mapper,
        IRoleService roleService)
    {
        _mapper = mapper;
        _roleService = roleService;
    }
    
    public async Task<GetRolesResponse> Handle(GetRolesByUserIdRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<Role, bool>> expression = r => r.Users.Any(u => u.Id == request.UserId);
        
        var models = await _roleService.GetAllByExpressionAsync(expression, cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetRoleResponse>>(models));
    }
}