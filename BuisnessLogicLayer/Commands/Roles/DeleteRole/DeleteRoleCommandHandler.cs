using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Roles;
using MediatR;

namespace BuisnessLogicLayer.Commands.Roles.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleRequest>
{
    private readonly IRoleService _roleService;

    public DeleteRoleCommandHandler(
        IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    public async Task Handle(DeleteRoleRequest request, CancellationToken cancellationToken)
    {
        await _roleService.DeleteAsync(request.Id, cancellationToken);
    }
}