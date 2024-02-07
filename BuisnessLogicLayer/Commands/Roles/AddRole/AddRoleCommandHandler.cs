using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Roles;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Roles.AddRole;

public class AddRoleCommandHandler : IRequestHandler<AddRoleRequest>
{
    private readonly IMapper _mapper;
    private readonly IRoleService _roleService;
    private readonly IValidator<AddRoleRequest> _validator;

    public AddRoleCommandHandler(
        IMapper mapper,
        IRoleService roleService,
        IValidator<AddRoleRequest> validator)
    {
        _mapper = mapper;
        _roleService = roleService;
        _validator = validator;
    }
    
    public async Task Handle(AddRoleRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<Role>(request);

        await _roleService.AddAsync(model, cancellationToken);
    }
}