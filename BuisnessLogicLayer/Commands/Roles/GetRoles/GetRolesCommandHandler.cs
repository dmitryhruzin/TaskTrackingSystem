using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Roles;
using BuisnessLogicLayer.Responses.Roles;
using MediatR;

namespace BuisnessLogicLayer.Commands.Roles.GetRoles;

public class GetRolesCommandHandler : IRequestHandler<GetRolesRequest, GetRolesResponse>
{
    private readonly IMapper _mapper;
    private readonly IRoleService _roleService;

    public GetRolesCommandHandler(
        IMapper mapper,
        IRoleService roleService)
    {
        _mapper = mapper;
        _roleService = roleService;
    }
    
    public async Task<GetRolesResponse> Handle(GetRolesRequest request, CancellationToken cancellationToken)
    {
        var models = await _roleService.GetAllAsync(cancellationToken);

        return new(_mapper.Map<IReadOnlyCollection<GetRoleResponse>>(models));
    }
}