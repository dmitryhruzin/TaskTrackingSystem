using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.ProjectStatuses;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.ProjectStatuses.UpdateProjectStatus;

public class UpdateProjectStatusCommandHandler : IRequestHandler<UpdateProjectStatusRequest>
{
    private readonly IMapper _mapper;
    private readonly IProjectStatusService _projectStatusService;
    private readonly IValidator<UpdateProjectStatusRequest> _validator;

    public UpdateProjectStatusCommandHandler(
        IMapper mapper,
        IProjectStatusService projectStatusService,
        IValidator<UpdateProjectStatusRequest> validator)
    {
        _mapper = mapper;
        _projectStatusService = projectStatusService;
        _validator = validator;
    }
    
    public async Task Handle(UpdateProjectStatusRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<ProjectStatus>(request);

        await _projectStatusService.UpdateAsync(model, cancellationToken);
    }
}