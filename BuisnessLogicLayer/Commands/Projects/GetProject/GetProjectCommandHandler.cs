using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Requests.Projects;
using BuisnessLogicLayer.Responses.Projects;
using MediatR;

namespace BuisnessLogicLayer.Commands.Projects.GetProject;

public class GetProjectCommandHandler : IRequestHandler<GetProjectRequest, GetProjectResponse>
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;

    public GetProjectCommandHandler(
        IMapper mapper,
        IProjectService projectService)
    {
        _mapper = mapper;
        _projectService = projectService;
    }
    
    public async Task<GetProjectResponse> Handle(GetProjectRequest request, CancellationToken cancellationToken)
    {
        var model = await _projectService.GetByIdAsync(request.Id, cancellationToken);

        return _mapper.Map<GetProjectResponse>(model);
    }
}