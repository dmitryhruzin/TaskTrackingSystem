using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Roles;
using BuisnessLogicLayer.Responses.Roles;
using MediatR;

namespace BuisnessLogicLayer.Commands.Roles.GetRole;

public class GetRoleCommandHandler : IRequestHandler<GetRoleRequest, GetRoleResponse>
{
    private readonly IMapper _mapper;
    private readonly IRoleService _roleService;

    public GetRoleCommandHandler(
        IMapper mapper,
        IRoleService roleService)
    {
        _mapper = mapper;
        _roleService = roleService;
    }
    
    public async Task<GetRoleResponse> Handle(GetRoleRequest request, CancellationToken cancellationToken)
    {
        var model = await _roleService.GetByIdAsync(request.Id, cancellationToken);

        return _mapper.Map<GetRoleResponse>(model);
    }
}