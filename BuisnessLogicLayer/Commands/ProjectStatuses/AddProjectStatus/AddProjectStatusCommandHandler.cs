using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.ProjectStatuses;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.ProjectStatuses.AddProjectStatus;

public class AddProjectStatusCommandHandler : IRequestHandler<AddProjectStatusRequest>
{
    private readonly IMapper _mapper;
    private readonly IProjectStatusService _projectStatusService;
    private readonly IValidator<AddProjectStatusRequest> _validator;

    public AddProjectStatusCommandHandler(
        IMapper mapper,
        IProjectStatusService projectStatusService,
        IValidator<AddProjectStatusRequest> validator)
    {
        _mapper = mapper;
        _projectStatusService = projectStatusService;
        _validator = validator;
    }
    
    public async Task Handle(AddProjectStatusRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<ProjectStatus>(request);

        await _projectStatusService.AddAsync(model, cancellationToken);
    }
}