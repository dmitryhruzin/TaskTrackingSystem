using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Projects;
using FluentValidation;
using MediatR;

namespace BuisnessLogicLayer.Commands.Projects.AddProject;

public class AddProjectCommandHandler : IRequestHandler<AddProjectRequest>
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;
    private readonly IValidator<AddProjectRequest> _validator;

    public AddProjectCommandHandler(
        IMapper mapper,
        IProjectService projectService,
        IValidator<AddProjectRequest> validator)
    {
        _mapper = mapper;
        _projectService = projectService;
        _validator = validator;
    }
    
    public async Task Handle(AddProjectRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var model = _mapper.Map<Project>(request);

        await _projectService.AddAsync(model, cancellationToken);
    }
}