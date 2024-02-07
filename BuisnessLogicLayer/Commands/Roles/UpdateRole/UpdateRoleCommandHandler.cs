using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Roles;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Roles.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleRequest>
{
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateRoleRequest> _validator;
    private readonly IRoleService _roleService;

    public UpdateRoleCommandHandler(
        IMapper mapper,
        IValidator<UpdateRoleRequest> validator,
        IRoleService roleService)
    {
        _mapper = mapper;
        _validator = validator;
        _roleService = roleService;
    }
    
    public async Task Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<Role>(request);

        await _roleService.UpdateAsync(model, cancellationToken);
    }
}